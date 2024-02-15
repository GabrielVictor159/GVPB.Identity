
using GVPB.Identity.Infraestructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVPB.Identity.Infraestructure.Database.Map;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "UserManagement");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
    }
}

