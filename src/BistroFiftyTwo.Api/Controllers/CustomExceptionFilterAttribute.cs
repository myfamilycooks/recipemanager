using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BistroFiftyTwo.Api.Controllers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
#if DEBUG
            context.Result = new JsonResult(new { Message = exception.Message, StackTrace = exception.StackTrace });
#else
            context.Result = new JsonResult(exception.Message);
#endif
        }
    }
}