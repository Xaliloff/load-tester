using System.Security.Cryptography;

namespace LoadTester.App.Services;

public class AuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IUserRepository _userRepository;

    public AuthService(ILogger<AuthService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<AppUser> CheckPassword(string userName, string enteredPassword, CancellationToken ct = default)
    {
        _logger.LogInformation("Checking password for user {user}", userName);

        var appUser = await _userRepository.GetByUserNameAsync(userName, ct);

        if (appUser == null) return null;

        var saltBytes = Convert.FromBase64String(appUser.Salt);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
        if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == appUser.PasswordHash) return appUser;

        return null;
    }

    public async Task Register(string userName, string password, CancellationToken ct = default)
    {
        _logger.LogInformation("Registering user {user}", userName);

        var saltBytes = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        var hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

        var appUser = new AppUser
        {
            UserName = userName,
            PasswordHash = hash,
            Salt = Convert.ToBase64String(saltBytes)
        };

        await _userRepository.AddAsync(appUser, ct);
    }
}