namespace Pastebin.API.Attributes.Swagger
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public class BaseProducesResponseTypeAttribute : Attribute
	{
		public int[] StatusCodes { get; } = [
			Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
			Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound,
			Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
		];

		public Type ResponseType { get; }

		public BaseProducesResponseTypeAttribute(Type responseType)
		{
			ResponseType = responseType;
		}

		public BaseProducesResponseTypeAttribute(Type responseType, params int[] statusCodes)
		{
			ResponseType = responseType;
			StatusCodes = statusCodes;
		}
	}
}
