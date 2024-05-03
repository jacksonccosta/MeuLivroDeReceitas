using MyRecipeBook.API.Filters;
using MyRecipeBook.API.Middleware;
using MyRecipeBook.Application;
using MyRecipeBook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
        return;

    var connetionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DataBaseMigration.Migrate(connetionString, serviceScope.ServiceProvider);
}

public partial class Program 
{
    protected Program(){}
}