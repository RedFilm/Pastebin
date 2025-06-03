using System.Net;

namespace Pastebin.Domain.Exceptions
{
	public class NotFoundException : StatusCodedException
	{
		public override int StatusCode => (int)HttpStatusCode.NotFound;

		public NotFoundException(string message = "NotFound", Exception? innerException = null)
			: base(message, innerException)
		{
		}

		public NotFoundException(object id, Exception? innerException = null)
			: this($"Entity with ID: {id} was not found.", innerException)
		{
		}
	}
}