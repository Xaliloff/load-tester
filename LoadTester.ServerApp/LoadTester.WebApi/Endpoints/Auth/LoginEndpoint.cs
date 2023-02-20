namespace LoadTester.WebApi.Endpoints.Auth;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    public AuthService AuthService { get; set; } = default!;
    public ICurrentUserService CurrentUserService { get; set; } = default!;
    public IUserRepository UserRepository { get; set; } = default!;

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await UserRepository.GetByUserNameAsync(request.UserName, ct);

        if (user != null)
        {
            if (await TryLogin(request, ct)) return;
        }
        else
        {
            await AuthService.Register(request.UserName, request.Password, ct);
            if (await TryLogin(request, ct)) return;
        }
        await SendUnauthorizedAsync(ct);
    }

    private async Task<bool> TryLogin(LoginRequest request, CancellationToken ct)
    {
        var authenticatedUser = await AuthService.CheckPassword(request.UserName, request.Password, ct);
        if (authenticatedUser == null)
        {
            await SendUnauthorizedAsync(ct);
            return false;
        }

        var jwtToken = JWTBearer.CreateToken(
                signingKey: "very-very-very-long-long-shit-for-security",
                expireAt: DateTime.UtcNow.AddDays(1),
                claims: new[] { (ClaimTypes.Name, authenticatedUser.UserName), (ClaimTypes.Sid, authenticatedUser.Id) });

        await SendAsync(new LoginResponse
        {
            Token = jwtToken
        }, StatusCodes.Status200OK, ct);

        return true;
    }
}