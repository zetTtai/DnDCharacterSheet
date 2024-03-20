namespace CleanArchitecture.Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
#if UseSQLite
        var database = new SqliteTestDatabase();
#else
#if DEBUG
        SqlServerTestDatabase database = new();
#else
        var database = new TestcontainersTestDatabase();
#endif
#endif

        await database.InitialiseAsync();

        return database;
    }
}
