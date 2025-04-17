using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<TestData> TestDataTable { get; set; }
    
    public DbSet<User> UserDataTable { get; set; }
    
    public DbSet<Event> EventDataTable { get; set; }
    
    public DbSet<UserEvent> UserEventDataTable { get; set; }
    
}