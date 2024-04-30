using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure;

public static class ConfigurationExtension
{
    public static string ConnectionString(this IConfiguration configuration) => configuration.GetConnectionString("DbConnectionSqlServer")!;
}
