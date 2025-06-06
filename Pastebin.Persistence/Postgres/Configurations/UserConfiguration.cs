using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pastebin.Domain.DbEntities;

namespace Pastebin.Persistence.Postgress.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasMany(u => u.Pastes)
				.WithOne(p => p.User)
				.HasForeignKey(paste => paste.UserId);

			builder.Property(x => x.UserName).HasMaxLength(64);
		}
	}
}
