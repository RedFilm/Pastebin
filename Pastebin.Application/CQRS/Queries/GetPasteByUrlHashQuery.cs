using MediatR;
using Pastebin.Domain.DbEntities;

namespace Pastebin.Application.CQRS.Queries
{
	public record GetPasteByUrlHashQuery(string Hash) : IRequest<Paste>;
}
