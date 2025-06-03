using MediatR;

namespace Pastebin.Application.CQRS.Commands
{
	public record CreatePasteCommand(
		string Content,
		string? Alias,
		DateTime LifeTime,
		Guid AuthorId
	) : IRequest<string>;
}
