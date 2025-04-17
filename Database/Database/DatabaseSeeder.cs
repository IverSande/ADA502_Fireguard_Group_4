using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseSeeder
{
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(ILogger<DatabaseSeeder> logger)
    {
        _logger = logger;
    }

    public void Seed(ApplicationDbContext dbContext)
    {
        dbContext.TestDataTable.Add(new TestData() { Temperature = "2" });
        dbContext.TestDataTable.Add(new TestData() { Temperature = "5" });
        dbContext.TestDataTable.Add(new TestData() { Temperature = "87" });
        dbContext.TestDataTable.Add(new TestData() { Temperature = "-234" });

        dbContext.UserDataTable.Add(new User { Username = "Test User" });
        dbContext.UserDataTable.Add(new User { Username = "Andreas Rovik" });

        dbContext.EventDataTable.Add(new Event { EventLocation = "Bergen" });
        dbContext.EventDataTable.Add(new Event { EventLocation = "Oslo" });
        
        dbContext.UserEventDataTable.Add(new UserEvent
        {
            EventId = 1, 
            UserId = 1 
        });

        dbContext.SaveChanges();
        
        _logger.LogInformation("Seeding data finished.");
        

    }
}