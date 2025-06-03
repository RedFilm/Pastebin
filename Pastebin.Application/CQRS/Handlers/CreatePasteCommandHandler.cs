using MediatR;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Domain.DbEntities;
using Pastebin.Domain.Interfaces.Repository;

namespace Pastebin.Application.CQRS.Handlers
{
	public class CreatePasteCommandHandler : IRequestHandler<CreatePasteCommand, long>
	{
		private readonly IPasteRepository _pasteRepository;

		public CreatePasteCommandHandler(IPasteRepository repository)
		{
			_pasteRepository = repository;
		}

		public async Task<long> Handle(CreatePasteCommand request, CancellationToken ct)
		{
			//var hash = await _hashService.GenerateHashAsync(request.Content);

			var paste = new Paste
			{
				//Hash = hash,
				UserId = request.AuthorId,
				CreationDate = DateTime.UtcNow,
				ExpirationDate = DateTimeOffset.UtcNow.AddDays(request.LifeTime.Day),
			};

			await _pasteRepository.AddAsync(paste);
			return paste.Id;
		}
	}
}
