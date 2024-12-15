using IdentityPersistance.Models;
using Microsoft.EntityFrameworkCore;

public class ProgramContext : DbContext
{
    public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
    {

    }
    public DbSet<Account> Users { get; set; }
    
}