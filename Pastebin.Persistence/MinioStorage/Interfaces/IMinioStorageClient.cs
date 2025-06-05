using Pastebin.Persistence.MinioStorage.Models;

namespace Pastebin.Persistence.MinioStorage
{
	/// <summary>
	/// Клиент для взаимодействия с Minio.
	/// </summary>
	public interface IMinioStorageClient
	{
		public Task<MinioObjectContent> GetObjectAsync(MinioObjectMetadata objectData,
			CancellationToken cancellationToken = default);

		public Task CreateObjectAsync(MinioObjectContent objectContent,
			CancellationToken cancellationToken = default);

		public Task RemoveObjectAsync(MinioObjectMetadata objectData,
			CancellationToken cancellationToken = default);

		public Task CreateBucketIfNotExistsAsync(string bucketName,
			CancellationToken cancellationToken = default);

		public Task<bool> CheckIfObjectExistsAsync(MinioObjectMetadata objectData,
			CancellationToken cancellationToken = default);
	}
}