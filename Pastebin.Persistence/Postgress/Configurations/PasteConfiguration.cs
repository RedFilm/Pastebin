using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pastebin.Domain.DbEntities;

namespace Pastebin.Persistence.Postgress.Configurations
{
    public class PasteConfiguration
    {
        public void Configure(EntityTypeBuilder<Paste> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UrlHash)
                .HasMaxLength(8);
            builder.HasIndex(x => x.UrlHash).IsUnique();
        }
    }
}
