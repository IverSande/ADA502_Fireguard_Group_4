using Api.Controllers;
using Api.Services;
using AuthenticationServiceClient;
using Grpc.Net.Client;
using RabbitMQ.Client;
using TestServiceClient;
using UserServiceClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configure gRPC client for database service
var dbServiceUrl = builder.Configuration["DatabaseService:Url"] ?? 
                Environment.GetEnvironmentVariable("DATABASE_SERVICE_URL") ?? 
                "https://database:5001";

var rabbitConnectionString = builder.Configuration.GetConnectionString("rabbit");

builder.Services.AddSingleton<IQueueService>(new QueueService(rabbitConnectionString));

builder.Services
    .AddGrpcClient<TestService.TestServiceClient>(o =>
    {
        o.Address = new Uri(dbServiceUrl);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        return handler;
    });
builder.Services
    .AddGrpcClient<AuthenticationService.AuthenticationServiceClient>(o =>
    {
        o.Address = new Uri(dbServiceUrl);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        return handler;
    });
builder.Services
    .AddGrpcClient<DBUserService.DBUserServiceClient>(o =>
    {
        o.Address = new Uri(dbServiceUrl);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        return handler;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();


app.Run();
