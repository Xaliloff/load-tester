global using System;
global using System.Linq;
global using System.Threading.Tasks;
global using FastEndpoints;
global using LoadTester.App;
global using LoadTester.App.Services;
global using LoadTester.App.Entities;
global using LoadTester.App.Repositories;
global using LoadTester.App.Services.ServiceRequests;
global using LoadTester.App.Services.ServiceResponses;
global using System.Security.Claims;
global using LoadTester.App.Interfaces;
global using FastEndpoints.Security;
using LoadTester.WebApi.Services;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LoadTester.Infrastructure.Migrations;
using LoadTester.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("very-very-very-long-long-shit-for-security")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/hubs")))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwaggerDoc()
    .AddHttpClient()
    .AddTransient<UserCreationService>()
    .AddTransient<AuthService>()
    .AddScoped<ICurrentUserService, CurrentUserService>()
    .AddScoped<LoadTestService>()
    .AddScoped<LoadTestProcess>()
    .AddSingleton<LoadTestContainer>()
    .AddSingleton<UserWorkflowService>()
    .AddSingleton<IRequestsRepository, RequestsRepository>()
    .AddSingleton<ITestConfigurationRepository, TestConfigurationRepository>()
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<ITestRepository, TestRepository>()
    .AddCors()
    .AddAuthorization()
    .AddFastEndpoints()
    .AddSignalR();

var app = builder.Build();

app.UseFastEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3(s => s.ConfigureDefaults());
}

app.UseCors(x => x.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials())
    .UseAuthentication()
    .UseAuthorization();

app.RunMigrations();
app.MapHub<TestRealDataHub>("hubs/testRealDataHub");

app.Run();