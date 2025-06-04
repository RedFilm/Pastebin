using Microsoft.OpenApi.Models;
using Pastebin.API.Attributes.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pastebin.API.Filters
{
	public class BaseProducesResponseTypeFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var attributes = context.MethodInfo.GetCustomAttributes(true)
				.OfType<BaseProducesResponseTypeAttribute>()
				.ToList();

			if (attributes.Count == 0)
			{
				attributes = context.MethodInfo.DeclaringType?
					.GetCustomAttributes(true)
					.OfType<BaseProducesResponseTypeAttribute>()
					.ToList();
			}

			if (attributes is null) return;

			foreach (var attribute in attributes)
			{
				var schema = context.SchemaGenerator.GenerateSchema(attribute.ResponseType, context.SchemaRepository);

				foreach (var statusCode in attribute.StatusCodes)
				{
					operation.Responses.TryAdd(
						statusCode.ToString(),
						new OpenApiResponse
						{
							Description = GetDescriptionForStatusCode(statusCode),
							Content = new Dictionary<string, OpenApiMediaType>
							{
								["application/json"] = new OpenApiMediaType
								{
									Schema = schema
								}
							}
						});
				}
			}
		}

		private string GetDescriptionForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				StatusCodes.Status400BadRequest => "Bad Request",
				StatusCodes.Status401Unauthorized => "Unauthorized",
				StatusCodes.Status403Forbidden => "Forbidden",
				StatusCodes.Status404NotFound => "Not Found",
				StatusCodes.Status500InternalServerError => "Internal Server Error",
				_ => "Unknown status code"
			};
		}
	}
}
