namespace Pastebin.Persistence.MinioStorage.Models
{
	/// <summary>
	/// Данные содержимого объекта.
	/// </summary>
	public class MinioObjectContent
	{
		public MinioObjectMetadata Metadata { get; set; } = null!;

		public string ContentType { get; set; } = string.Empty;
		public MemoryStream Stream { get; set; } = null!;
    }
}