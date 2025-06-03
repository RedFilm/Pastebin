using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pastebin.Domain.Exceptions
{
	public class ForbiddenException : StatusCodedException
	{
		public override int StatusCode => (int)HttpStatusCode.Forbidden;

		public ForbiddenException(string message = "Forbidden", Exception? innerException = null)
			: base(message, innerException)
		{
		}
	}
}
