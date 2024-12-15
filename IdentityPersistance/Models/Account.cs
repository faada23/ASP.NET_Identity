
namespace IdentityPersistance.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
}