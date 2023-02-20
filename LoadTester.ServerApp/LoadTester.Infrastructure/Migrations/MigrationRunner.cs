using System.Reflection;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoadTester.Infrastructure.Migrations;

public static class MigrationRunner
{
    public static void RunMigrations(this IHost app)
    {
        var configuration = app.Services.GetService<IConfiguration>() ?? throw new Exception();
        var connectionString = configuration.GetConnectionString("Default") ?? throw new Exception();

        var assembly = Assembly.GetExecutingAssembly();

        var derivedTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Migration)))
            .ToArray();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var lastMigration = connection.Query(@"CREATE TABLE IF NOT EXISTS Migrations (
                                    Id TEXT PRIMARY KEY,
                                    UserName TEXT,
                                    PasswordHash TEXT,
                                    Salt TEXT
                                );");
        foreach (var type in derivedTypes)
        {
            var instance = (Migration)Activator.CreateInstance(type)!;
            instance.Do(connectionString);
        }
    }
}