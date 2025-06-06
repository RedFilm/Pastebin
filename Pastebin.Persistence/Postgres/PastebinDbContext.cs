using Microsoft.EntityFrameworkCore;
using Pastebin.Domain.DbEntities;
using Pastebin.Persistence.Postgress.Configurations;

namespace Pastebin.Persistence.Postgres
{
    public class PastebinDbContext : DbContext
	{
		public PastebinDbContext(DbContextOptions<PastebinDbContext> options) : base (options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PasteConfiguration).Assembly);

			base.OnModelCreating(modelBuilder);
		}

        public DbSet<Paste> Pastes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
