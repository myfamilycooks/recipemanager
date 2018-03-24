using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BistroFiftyTwo.Api.Controllers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.HttpContext.Response.StatusCode = 500;
#if DEBUG
            context.Result = new JsonResult(new {exception.Message, exception.StackTrace});
#else
            context.Result = new JsonResult(exception.Message);
#endif
        }
    }
}