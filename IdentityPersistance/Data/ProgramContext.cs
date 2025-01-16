using IdentityPersistance.Models;
using Microsoft.EntityFrameworkCore;

public class ProgramContext : DbContext
{
    public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
    {
       
    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       modelBuilder.Entity<Role>(b =>
        {
            b.HasData(
                new Role { Id = 1, Name = "Admin" },  
                new Role { Id = 2, Name = "User" }
            );
            
        });
     
}}