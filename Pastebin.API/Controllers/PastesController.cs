using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Application.CQRS.Queries;


namespace Pastebin.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PastesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PastesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPaste(long id)
		{
			var paste = await _mediator.Send(new GetPasteByIdQuery(id));
			return Ok(paste);
		}

		[HttpPost]
		public async Task<IActionResult> CreatePaste([FromBody] CreatePasteCommand command)
		{
			var pasteId = await _mediator.Send(command);
			return Ok(new { Id = pasteId });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePaste(long id, [FromHeader] Guid authorId)
		{
			await _mediator.Send(new DeletePasteCommand(id, authorId));
			return NoContent();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePaste( long id, 
			[FromBody] UpdatePasteCommand command,
			[FromHeader] Guid authorId)
		{
			command = command with { Id = id, AuthorId = authorId };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
