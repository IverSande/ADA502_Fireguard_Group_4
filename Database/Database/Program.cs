using Database;
using Database.TestService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<DatabaseSeeder>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Psql")));

builder.Services.AddGrpc();

var app = builder.Build();

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    if (app.Environment.IsDevelopment())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        seeder.Seed(dbContext);
        
    }
}

app.MapGrpcService<TestService>().AllowAnonymous();

app.Run();