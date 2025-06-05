namespace Pastebin.Persistence.MinioStorage.Options
{
	public class MinioOptions
	{
		public const string SectionPath = "MinioOptions";

		public string Host { get; set; } = default!;

		public int? Port { get; set; }

		public string AccessKey { get; set; } = default!;

		public string SecretKey { get; set; } = default!;

		public bool UseSsl { get; set; }

		public string[]? BucketNames { get; set; }
	}
}