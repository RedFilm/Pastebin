using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pastebin.Persistence.MinioStorage.Options;

namespace Pastebin.Persistence.MinioStorage.DependencyInjection
{
	public static class MinioExtensions
	{
		/// <summary>
		/// Добавление клиента Minio.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions().Configure((Action<MinioOptions>)
				(options => configuration.GetSection(MinioOptions.SectionPath).Bind(options)));

			services.AddSingleton<IMinioStorageClient, MinioStorageClient>();
			return services;
		}
	}
}