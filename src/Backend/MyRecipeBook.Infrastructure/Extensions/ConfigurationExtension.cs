﻿using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure;

public static class ConfigurationExtension
{
    public static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }

    public static string ConnectionString(this IConfiguration configuration) => configuration.GetConnectionString("DbConnectionSqlServer")!;
}
