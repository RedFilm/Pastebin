namespace Pastebin.Application.Helpers
{
	public static class S3StorageHelper
	{
		public static string GetS3BucketName(string hash)
		{
			return $"content-s3-bucket-{hash}";
		}
	}
}
