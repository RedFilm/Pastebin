using MediatR;
using Pastebin.Application.CQRS.Commands;
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

			// TODO: custom exceptions
			//if (paste == null) throw new NotFoundException("Paste not found");

			//if (paste.UserId != command.AuthorId)
				//throw new ForbiddenException("You can't edit this paste");

			paste.ExpirationDate.AddDays(command.LifeTime.Day);

			await _repository.UpdateAsync(paste);

			return Unit.Value;
		}
	}
}
