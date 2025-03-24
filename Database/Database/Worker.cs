namespace Database;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1000, stoppingToken);
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        
    }
}