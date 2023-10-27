
using Microsoft.EntityFrameworkCore;
namespace GVPB.Identity.Infraestructure.Database;

public class Context : DbContext
{
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
        base.OnModelCreating(modelBuilder);
    }
}

