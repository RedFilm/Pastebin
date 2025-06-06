using MediatR;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Domain.DbEntities;
using Pastebin.Domain.Exceptions;
using Pastebin.Domain.Interfaces.Repository;
using Pastebin.Persistence.MinioStorage;
using Pastebin.Persistence.MinioStorage.Helpers;
using Pastebin.Persistence.MinioStorage.Models;
using System.Text;

namespace Pastebin.Application.CQRS.Handlers
{
	public class CreatePasteCommandHandler : IRequestHandler<CreatePasteCommand, string>
	{
		private const string BucketName = "text-content-bucket";
		private const string ContentType = "text/plain";

		private readonly IMinioStorageClient _minioStorageClient;
		private readonly IPasteRepository _pasteRepository;

		public CreatePasteCommandHandler(IMinioStorageClient minioStorageClient,
			IPasteRepository repository)
		{
			_minioStorageClient = minioStorageClient;
			_pasteRepository = repository;
		}

		public async Task<string> Handle(CreatePasteCommand request, CancellationToken ct)
		{
			string urlHash = string.Empty;

			if (request.Alias is not null)
			{
				if (await _pasteRepository.AnyAsync(request.Alias))
					throw new ConflictException("Alias already exists. Try another one.");
				else
					urlHash = request.Alias;
			}

			// TODO: Check if hash is unique?
			if (urlHash == string.Empty)
				urlHash = GenerateHashAsync();

			var authorId = request.AuthorId;
			var textBytes = Encoding.UTF8.GetBytes(request.Content);
			using var stream = new MemoryStream(textBytes);

			var minioObjContent = new MinioObjectContent
			{
				Metadata = new MinioObjectMetadata { BucketName = BucketName, ObjectName =  urlHash, Prefix = authorId.ToString() },
				ContentType = ContentType,
				Stream = stream
			};

			await _minioStorageClient.CreateObjectAsync(minioObjContent);

			var contentPath = string.Concat(BucketName, MinioPathHelper.GetMinioObjectPath(urlHash, authorId.ToString()));
			var paste = new Paste
			{
				UrlHash = urlHash,
				UserId = request.AuthorId,
				ContentPath = contentPath,
				CreationDate = DateTime.UtcNow,
				ExpirationDate = DateTimeOffset.UtcNow.AddDays(request.LifeTime.Day),
			};

			await _pasteRepository.AddAsync(paste);

			return urlHash;
		}

		// Temporary bad solution
		// TODO: make hash generation service
		private string GenerateHashAsync()
		{
			var random = Random.Shared;

			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, 8)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
