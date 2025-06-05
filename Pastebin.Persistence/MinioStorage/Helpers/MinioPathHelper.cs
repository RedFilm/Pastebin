namespace Pastebin.Persistence.MinioStorage.Helpers
{
	public static class MinioPathHelper
	{
		public const char BackSlashSymbol = '\\';

		public const char ForwardSlashSymbol = '/';

		/// <summary>
		/// Возвращает путь к объекту в Minio.
		/// </summary>
		/// <param name="objectName"></param>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public static string GetMinioObjectPath(string objectName, string? prefix = null)
		{
			if (string.IsNullOrEmpty(prefix))
				return objectName;

			var path = Path.Combine(prefix, objectName).Replace(BackSlashSymbol, ForwardSlashSymbol);
			return path;
		}
	}
}