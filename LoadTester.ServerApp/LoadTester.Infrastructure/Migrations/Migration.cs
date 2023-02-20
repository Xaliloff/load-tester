using System.Data.SqlClient;

namespace LoadTester.Infrastructure.Migrations;

public abstract class Migration
{
    public abstract void Do(string connectionString);
}