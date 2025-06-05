namespace Pastebin.Persistence.MinioStorage.Models
{
	/// <summary>
	/// Метаданные объекта в Minio.
	/// </summary>
	public class MinioObjectMetadata
	{
		public string BucketName { get; set; } = string.Empty;

		public string ObjectName { get; set; } = string.Empty;

		public string? Prefix { get; set; }
	}
}