using MediatR;

namespace Pastebin.Application.CQRS.Commands
{
	public record CreatePasteCommand(
		string Content,
		DateTime LifeTime,
		Guid AuthorId
	) : IRequest<long>;
}
