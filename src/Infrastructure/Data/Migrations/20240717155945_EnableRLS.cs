using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations;

public partial class EnableRLS() : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var tableNames = GetAllPublicTableNames();
        foreach (var name in tableNames)
        {
            migrationBuilder.Sql($"ALTER TABLE \"{name}\" ENABLE ROW LEVEL SECURITY;");
        }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        var tableNames = GetAllPublicTableNames();
        foreach (var name in tableNames)
        {
            migrationBuilder.Sql($"ALTER TABLE \"{name}\" DISABLE ROW LEVEL SECURITY;");
        }
    }

    private static List<string> GetAllPublicTableNames()
    {
        var tableNames = new List<string>();

        string connectionString = Environment.GetEnvironmentVariable("APPSETTING_CONNECTION_STRING") ??
            throw new Exception("Unable to set the connection, set APPSETTING_CONNECTION_STRING environment variable");

        string query = @"
            SELECT table_name
            FROM information_schema.tables
            WHERE table_schema = 'public'
            AND table_type = 'BASE TABLE'";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string tableName = reader.GetString(0);
                tableNames.Add(tableName);
            }
        }

        return tableNames;
    }
}
