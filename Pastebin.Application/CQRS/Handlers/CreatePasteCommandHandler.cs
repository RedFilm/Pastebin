using MediatR;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Application.Helpers;
using Pastebin.Domain.DbEntities;
using Pastebin.Domain.Exceptions;
using Pastebin.Domain.Interfaces.Repository;

namespace Pastebin.Application.CQRS.Handlers
{
	public class CreatePasteCommandHandler : IRequestHandler<CreatePasteCommand, string>
	{
		private readonly IPasteRepository _pasteRepository;

		public CreatePasteCommandHandler(IPasteRepository repository)
		{
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

			var paste = new Paste
			{
				UrlHash = urlHash,
				UserId = request.AuthorId,
				BucketKey = S3StorageHelper.GetS3BucketName(urlHash),
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
