using System.Net;

namespace Pastebin.Domain.Exceptions
{
	public class ConflictException : StatusCodedException
	{
		public override int StatusCode => (int)HttpStatusCode.Conflict;

		public ConflictException(string? message = "Conflict", Exception? innerException = null)
			: base(message, innerException)
		{
		}
	}
}
