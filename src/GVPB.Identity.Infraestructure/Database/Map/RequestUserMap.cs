
using GVPB.Identity.Infraestructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVPB.Identity.Infraestructure.Database.Map;

public class RequestUserMap : IEntityTypeConfiguration<RequestUser>
{
    public void Configure(EntityTypeBuilder<RequestUser> builder)
    {
        builder.ToTable("RequestUsers", "UserManagement");
        builder.HasKey(x => x.Id);
    }
}

