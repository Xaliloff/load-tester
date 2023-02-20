namespace LoadTester.App.Entities;

public class AppUser
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
}