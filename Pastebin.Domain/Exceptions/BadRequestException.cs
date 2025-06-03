using System.Net;

namespace Pastebin.Domain.Exceptions
{
	public class BadRequestException : StatusCodedException
	{
		public override int StatusCode => (int)HttpStatusCode.BadRequest;

		protected BadRequestException(string? message = "BadRequest", Exception? innerException = null)
			: base(message, innerException)
		{
		}
	}
}