using MediatR;

namespace Pastebin.Application.CQRS.Commands
{
	public record DeletePasteCommand(
		long Id,
		Guid AuthorId
	) : IRequest<Unit>;
}
