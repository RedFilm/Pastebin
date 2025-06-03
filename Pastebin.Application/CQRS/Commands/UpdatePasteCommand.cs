using MediatR;

namespace Pastebin.Application.CQRS.Commands
{
	public record UpdatePasteCommand(
		long Id,
		string Content,
		DateTime LifeTime,
		Guid AuthorId
	) : IRequest<Unit>;
}
