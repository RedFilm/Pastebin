namespace Pastebin.Domain.Exceptions
{
	public abstract class StatusCodedException : Exception
	{
		public abstract int StatusCode { get; }

		protected StatusCodedException(string? message = null, Exception? innerException = null)
			: base(message, innerException)
		{
		}
	}
}