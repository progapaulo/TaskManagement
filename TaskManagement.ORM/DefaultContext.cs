using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TaskManagement.Domain.Entities;
using System.Reflection;


namespace TaskManagement.ORM;

public class DefaultContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<TaskHistory> TaskHistories { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
            connectionString,
            b => b.MigrationsAssembly("TaskManagement.ORM")
        );

        return new DefaultContext(builder.Options);
    }
}