using MediatR;
using Pastebin.Application.CQRS.Queries;
using Pastebin.Domain.DbEntities;
using Pastebin.Domain.Interfaces.Repository;

namespace Pastebin.Application.CQRS.Handlers
{
	public class GetPasteByIdQueryHandler : IRequestHandler<GetPasteByIdQuery, Paste>
	{
		private readonly IPasteRepository _pasteRepository;

		public GetPasteByIdQueryHandler(IPasteRepository repository)
			=> _pasteRepository = repository;

		public async Task<Paste> Handle(GetPasteByIdQuery request, CancellationToken ct)
		{
			var paste = await _pasteRepository.GetByIdAsync(request.Id);

			// TODO: custom exceptions
			//if (paste == null) throw new NotFoundException("Paste not found");
			return paste;
		}
	}
}
