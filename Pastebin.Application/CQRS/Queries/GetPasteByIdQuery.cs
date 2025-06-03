using MediatR;
using Pastebin.Domain.DbEntities;

namespace Pastebin.Application.CQRS.Queries
{
	public record GetPasteByIdQuery(long Id) : IRequest<Paste>;
}
