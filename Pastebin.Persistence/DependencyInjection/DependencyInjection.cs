using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pastebin.Domain.Interfaces.Repository;
using Pastebin.Persistence.Repository;

namespace Pastebin.Persistence.DependencyInjection
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Добавляет слой Persistence.
		/// </summary>
		public static IServiceCollection AddPersistence(this IServiceCollection services,
			IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("PastebinDB");

			if (string.IsNullOrWhiteSpace(connectionString))
				throw new InvalidOperationException("Connection string was not provided.");

			services
				.AddDbContext<PastebinDbContext>(options => options.UseNpgsql(connectionString))
				.AddRepositories();

			return services;
		}

		/// <summary>
		/// Добавляет репозитории.
		/// </summary>
		private static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IPasteRepository, PasteRepository>();

			return services;
		}
	}
}
