using Microsoft.Extensions.DependencyInjection;
using Pastebin.Application.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
