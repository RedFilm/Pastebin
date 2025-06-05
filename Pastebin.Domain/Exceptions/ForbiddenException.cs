using System.Net;

namespace Pastebin.Domain.Exceptions
{
	public class ForbiddenException : StatusCodedException
	{
		public override int StatusCode => (int)HttpStatusCode.Forbidden;

		public ForbiddenException(string message = "Forbidden", Exception? innerException = null)
			: base(message, innerException)
		{
		}
	}
}