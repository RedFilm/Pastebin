using Microsoft.Extensions.DependencyInjection;
using Pastebin.Application.CQRS.Commands;

namespace Pastebin.Application.DependencyInjection
{
	/// <summary>
	/// Внедрение зависимостей слоя Application.
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// Добавляет слой Persistence.
		/// </summary>
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMediatR(config =>
				config.RegisterServicesFromAssembly(typeof(CreatePasteCommand).Assembly));

			return services;
		}
	}
}
