using MediatR;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Domain.Exceptions;
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

			if (paste is null) 
				throw new NotFoundException(command.Id);

			if (paste.UserId != command.AuthorId)
				throw new ForbiddenException("Permission denied.");

			await _repository.DeleteAsync(command.Id);

			return Unit.Value;
		}
	}
}
