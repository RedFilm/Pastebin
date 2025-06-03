using MediatR;
using Pastebin.Application.CQRS.Queries;
using Pastebin.Domain.DbEntities;
using Pastebin.Domain.Exceptions;
using Pastebin.Domain.Interfaces.Repository;

namespace Pastebin.Application.CQRS.Handlers
{
	public class GetPasteByHashUrlQueryHandler : IRequestHandler<GetPasteByUrlHashQuery, Paste>
	{
		private readonly IPasteRepository _pasteRepository;

		public GetPasteByHashUrlQueryHandler(IPasteRepository repository)
			=> _pasteRepository = repository;

		public async Task<Paste> Handle(GetPasteByUrlHashQuery request, CancellationToken ct)
		{
			var paste = await _pasteRepository.GetByUrlHashAsync(request.Hash);

			if (paste is null)
				throw new NotFoundException("Paste not found.");

			return paste;
		}
	}
}
