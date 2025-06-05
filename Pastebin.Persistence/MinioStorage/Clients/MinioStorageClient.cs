using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Pastebin.Persistence.MinioStorage.Helpers;
using Pastebin.Persistence.MinioStorage.Models;
using Pastebin.Persistence.MinioStorage.Options;

namespace Pastebin.Persistence.MinioStorage
{
	public class MinioStorageClient : IMinioStorageClient
	{
		private readonly IMinioClient _minioClient;

		public MinioStorageClient(IOptions<MinioOptions> minioOptions)
		{
			var minioOptionsValue = minioOptions.Value;

			_minioClient = new MinioClient()
				.WithEndpoint(minioOptionsValue.Host)
				.WithCredentials(minioOptionsValue.AccessKey, minioOptionsValue.SecretKey)
				.WithSSL(minioOptionsValue.UseSsl)
				.Build();
		}

		public async Task<bool> CheckIfObjectExistsAsync(MinioObjectMetadata objectData,
			CancellationToken cancellationToken = default)
		{
			try
			{
				var objectPath = MinioPathHelper.GetMinioObjectPath(objectData.ObjectName, objectData.Prefix);
				var statObjectArgs = new StatObjectArgs()
					.WithBucket(objectData.BucketName)
					.WithObject(objectPath);

				await _minioClient.StatObjectAsync(statObjectArgs, cancellationToken);

				return true;
			}
			catch (BucketNotFoundException)
			{
				return false;
			}
			catch (ObjectNotFoundException)
			{
				return false;
			}
		}

		public async Task CreateBucketIfNotExistsAsync(string bucketName,
			CancellationToken cancellationToken = default)
		{
			var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);

			if (await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken))
				return;

			var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);

			await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
		}

		public async Task CreateObjectAsync(MinioObjectContent objectContent,
			CancellationToken cancellationToken = default)
		{
			var objectPath = MinioPathHelper
				.GetMinioObjectPath(objectContent.Metadata.ObjectName, objectContent.Metadata.Prefix);

			var putObjectArgs = new PutObjectArgs()
				.WithBucket(objectContent.Metadata.BucketName)
				.WithObject(objectPath)
				.WithStreamData(objectContent.Stream)
				.WithObjectSize(objectContent.Stream.Length)
				.WithContentType(objectContent.ContentType);

			await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
		}

		public async Task<MinioObjectContent> GetObjectAsync(MinioObjectMetadata objectData,
			CancellationToken cancellationToken = default)
		{
			var stream = new MemoryStream();
			var objectPath = MinioPathHelper.GetMinioObjectPath(objectData.ObjectName, objectData.Prefix);
			var getObjectArgs = new GetObjectArgs()
				.WithBucket(objectData.BucketName)
				.WithObject(objectPath)
				.WithCallbackStream(stream.CopyToAsync);

			var minioObject = await _minioClient.GetObjectAsync(getObjectArgs, cancellationToken);

			var minioObjectContent = new MinioObjectContent
			{
				ContentType = minioObject.ContentType,
				Stream = stream,
				Metadata = objectData
			};

			return minioObjectContent;
		}

		public async Task RemoveObjectAsync(MinioObjectMetadata objectData,
			CancellationToken cancellationToken = default)
		{
			var objectPath = MinioPathHelper.GetMinioObjectPath(objectData.ObjectName, objectData.Prefix);
			var removeObjectArgs = new RemoveObjectArgs()
				.WithBucket(objectData.BucketName)
				.WithObject(objectPath);

			await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
		}
	}
}