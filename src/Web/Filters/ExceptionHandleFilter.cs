using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Web.Model;

namespace Web.Filters
{
	public class HandleExceptionAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Result = new JsonResult(
				new RequestResult
				{
					IsSuccess = false,
					ErrorMessage = context.Exception.Message
				});

			context.ExceptionHandled = true;

			base.OnException(context);
		}
	}
}
