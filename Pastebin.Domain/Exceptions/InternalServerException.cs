using System.Net;

namespace Pastebin.Domain.Exceptions
{
	public class InternalServerException : StatusCodedException
	{
		public override int StatusCode => (int)HttpStatusCode.InternalServerError;

		public InternalServerException(string message = "InternalServerError", Exception? innerException = null)
			: base(message, innerException)
		{
		}
	}
}