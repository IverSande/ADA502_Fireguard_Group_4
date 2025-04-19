namespace Database.Entities;

public class AccessToken
{
    public int Id { get; set; }
    
    public string Token { get; set; }
    
    public DateTimeOffset Expires { get; set; }
}