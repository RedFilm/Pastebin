using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pastebin.Domain.DbEntities;

namespace Pastebin.Persistence.Configurations
{
	public class PasteConfiguration
	{
		public void Configure(EntityTypeBuilder<Paste> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.PastePath).HasMaxLength(64);
		}
	}
}
