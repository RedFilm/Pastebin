using MediatR;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Domain.Exceptions;
using Pastebin.Domain.Interfaces.Repository;

namespace Pastebin.Application.CQRS.Handlers
{
	public class UpdatePasteCommandHandler : IRequestHandler<UpdatePasteCommand, Unit>
	{
		private readonly IPasteRepository _repository;

		public UpdatePasteCommandHandler(IPasteRepository repository)
			=> _repository = repository;

		public async Task<Unit> Handle(UpdatePasteCommand command, CancellationToken ct)
		{
			var paste = await _repository.GetByIdAsync(command.Id);

			if (paste is null)
				throw new NotFoundException("Paste not found.");

			if (paste.UserId != command.AuthorId)
				throw new ForbiddenException("Permission denied.");

			// TODO: change life time concept
			paste.ExpirationDate.AddDays(command.LifeTime.Day);

			await _repository.UpdateAsync(paste);

			return Unit.Value;
		}
	}
}
