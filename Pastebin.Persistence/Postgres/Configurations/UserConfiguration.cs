using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pastebin.Domain.DbEntities;

namespace Pastebin.Persistence.Postgress.Configurations
{
    public class UserConfiguration
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany<Paste>()
            .WithOne()
            .HasForeignKey(paste => paste.Id)
            .HasPrincipalKey(user => user.Id);

            builder.Property(x => x.UserName).HasMaxLength(64);
        }
    }
}
