namespace IdentityPersistance.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<Role> Roles {get;set;} = new List<Role>();
}