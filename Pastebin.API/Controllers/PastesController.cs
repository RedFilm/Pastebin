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
		private readonly IHttpContextAccessor _httpContextAccessor;

		public PastesController(IMediator mediator,
			IHttpContextAccessor httpContextAccessor)
		{
			_mediator = mediator;
			_httpContextAccessor = httpContextAccessor;
		}

		[HttpGet("{hashUrl}")]
		public async Task<IActionResult> GetPaste(string hashUrl)
		{
			var paste = await _mediator.Send(new GetPasteByUrlHashQuery(hashUrl));
			return Ok(paste);
		}

		[HttpPost]
		public async Task<IActionResult> CreatePaste([FromBody] CreatePasteCommand command)
		{
			var urlHash = await _mediator.Send(command);

			var baseUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://" +
				$"{_httpContextAccessor.HttpContext?.Request.Host}";

			var fullUrl = $"{baseUrl}/pastes/{urlHash}";

			return Ok(new { Url = fullUrl });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePaste(long id, [FromHeader] Guid authorId)
		{
			await _mediator.Send(new DeletePasteCommand(id, authorId));
			return NoContent();
		}


		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdatePaste( long id, 
			[FromBody] UpdatePasteCommand command,
			[FromHeader] Guid authorId)
		{
			command = command with { Id = id, AuthorId = authorId }; // TODO: Get user id from jwt
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
