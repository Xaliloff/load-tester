using Dapper;
using Microsoft.Data.Sqlite;

namespace LoadTester.Infrastructure.Migrations;

public class Migration1 : Migration
{
    public override void Do(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            var a = connection.Execute(@"CREATE TABLE IF NOT EXISTS LoadTestConfigurations (
                                Id TEXT PRIMARY KEY,
                                AuthorId TEXT NOT NULL,
                                Name TEXT,
                                Url TEXT,
                                Method TEXT,
                                Headers TEXT,
                                Body TEXT,
                                ConcurrentUsers INTEGER DEFAULT 2,
                                NewUsersEveryXSec INTEGER DEFAULT 10,
                                IncreaseUsersBy INTEGER DEFAULT 2,
                                DurationInSeconds INTEGER NOT NULL,
                                IsDeleted INTEGER DEFAULT 0,
                                Tests TEXT
                            );",
                transaction);

            connection.Execute(@"CREATE TABLE IF NOT EXISTS LoadTestProcessInfos (
                                Id TEXT PRIMARY KEY,
                                Name TEXT,
                                Status TEXT,
                                StartDate TEXT,
                                EndTime TEXT,
                                Requests TEXT,
                                ConfigurationId TEXT,
                                FOREIGN KEY(ConfigurationId) REFERENCES LoadTestConfigurations(Id)
                            );", transaction);
            
            connection.Execute(@"CREATE TABLE IF NOT EXISTS AppUsers (
                                    Id TEXT PRIMARY KEY,
                                    UserName TEXT,
                                    PasswordHash TEXT,
                                    Salt TEXT
                                );", transaction);

            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }
}