using Dapper;
using Microsoft.Data.SqlClient;

namespace MyRecipeBook.Infrastructure;

public class DataBaseMigration
{
    public static void Migrate(string connectionString)
    {
        EnsureDataBaseCreated(connectionString);
    }

    private static void EnsureDataBaseCreated(string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var dataBaseName = connectionStringBuilder.InitialCatalog;
        connectionStringBuilder.Remove("DataBase");

        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters(connectionStringBuilder);
        parameters.Add("name", dataBaseName);

        var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if (!records.Any())
            dbConnection.Execute($"CREATE DATABASE {dataBaseName}");
    }
}
