using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructure;

public class DataBaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        EnsureDataBaseCreated(connectionString);
        MigrationDataBase(serviceProvider);
    }

    protected DataBaseMigration()
    {
        
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

    private static void MigrationDataBase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}
