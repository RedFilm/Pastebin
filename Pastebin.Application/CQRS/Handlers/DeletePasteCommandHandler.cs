using MediatR;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Domain.Interfaces.Repository;

namespace Pastebin.Application.CQRS.Handlers
{
	public class DeletePasteCommandHandler : IRequestHandler<DeletePasteCommand, Unit>
	{
		private readonly IPasteRepository _repository;

		public DeletePasteCommandHandler(IPasteRepository repository)
			=> _repository = repository;

		public async Task<Unit> Handle(DeletePasteCommand command, CancellationToken ct)
		{
			var paste = await _repository.GetByIdAsync(command.Id);

			// TODO: custom exceptions
			//if (paste == null) throw new NotFoundException("Paste not found");

			//if (paste.AuthorId != command.AuthorId)
				//throw new ForbiddenException("You can't delete this paste");

			await _repository.DeleteAsync(command.Id);
			return Unit.Value;
		}
	}
}
