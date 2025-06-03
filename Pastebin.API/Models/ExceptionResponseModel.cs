using System.Text.Json;

namespace Pastebin.API.Models
{
	public class ExceptionResponseModel
	{
		public string Title { get; set; } = string.Empty;

		public string Request { get; set; } = string.Empty;

		public int StatusCode { get; set; }

		public DateTimeOffset Time { get; set; } = DateTimeOffset.UtcNow;

		public string? Detail { get; set; }

		public string? Trace { get; set; }

		public override string ToString() => JsonSerializer.Serialize(this);
	}
}
