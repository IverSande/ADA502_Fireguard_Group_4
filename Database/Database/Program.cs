using Database;
using Database.GRPCServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Psql")));

builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<DatabaseSeeder>();

builder.Services.AddGrpc();

var app = builder.Build();

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var retryCounter = 0;
    while (retryCounter < 60)
    {
        try
        {
            dbContext.Database.Migrate();
            break;
        }
        catch
        {
            retryCounter++;
            Task.Delay(1000).Wait();
        }
    }
    if (app.Environment.IsDevelopment())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        seeder.Seed(dbContext);
        
    }
}

app.MapGrpcService<TestService>().AllowAnonymous();
app.MapGrpcService<DbUserService>().AllowAnonymous();
app.MapGrpcService<AuthenticationService>().AllowAnonymous();

app.Run();