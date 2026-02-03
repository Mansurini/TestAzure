namespace MongoDb.WebApi.Documents;

public class User
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NewField { get; set; }
}
