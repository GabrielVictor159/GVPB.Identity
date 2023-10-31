
using GVPB.Identity.Infraestructure.Database.Entities;
using GVPB.Identity.Infraestructure.Database.Map;
using ManagementServices.variables.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
namespace GVPB.Identity.Infraestructure.Database;

public class Context : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<RequestUser> RequestUsers => Set<RequestUser>();
    public DbSet<EnvVariable> envVariables => Set<EnvVariable>();
    public DbSet<Log> Logs => Set<Log>(); 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Environment.GetEnvironmentVariable("DBCONN") != null)
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DBCONN"), options =>
            {
                options.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), new List<string>());
                options.MigrationsHistoryTable("_MigrationHistory", "Migrations");
            });
        else
            optionsBuilder.UseInMemoryDatabase("TelegramBotInMemory");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RequestUserMap());
        modelBuilder.ApplyConfiguration(new LogMap());
        base.OnModelCreating(modelBuilder);
    }
}

