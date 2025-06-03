using Microsoft.AspNetCore.Diagnostics;
using Pastebin.API.Models;
using Pastebin.Domain.Exceptions;
using System.Net;

namespace Pastebin.API.Handlers
{
	public class GlobalExceptionHandler : IExceptionHandler
	{
		private readonly ILogger<GlobalExceptionHandler> _logger;
		private readonly IWebHostEnvironment _hostEnvironment;


		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
			IWebHostEnvironment hostEnvironment)
		{
			_logger = logger;
			_hostEnvironment = hostEnvironment;
		}


		public async ValueTask<bool> TryHandleAsync(
			HttpContext context,
			Exception exception,
			CancellationToken cancellationToken)
		{
			_logger.LogError(exception, "Произошло исключение: {Message}", exception.Message);

			var (statusCode, title) = exception switch
			{
				BadRequestException => (HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString()),
				NotFoundException => (HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString()),
				_ => (HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString())
			};


			var exceptionModel = new ExceptionResponseModel
			{
				Title = title,
				Detail = exception.Message,
				Request = $"{context.Request.Method} {context.Request.Path}",
				StatusCode = (int)statusCode,
				Time = DateTimeOffset.Now,
				Trace = _hostEnvironment.IsDevelopment() ? exception.StackTrace : string.Empty
			};

			context.Response.StatusCode = (int)statusCode;
			await context.Response.WriteAsJsonAsync(exceptionModel, cancellationToken);

			return true;
		}
	}
}
