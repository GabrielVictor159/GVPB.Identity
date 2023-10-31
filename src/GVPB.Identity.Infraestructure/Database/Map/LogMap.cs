
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GVPB.Identity.Infraestructure.Database.Entities;

namespace GVPB.Identity.Infraestructure.Database.Map;

public class LogMap : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("Logs", "UserManagement");
        builder.HasKey(x => x.Id);
    }
}

