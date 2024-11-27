using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ArgumentException argumentException:
                SetMessageAndMarkHandled(context, argumentException.Message, StatusCodes.Status400BadRequest);
                break;
            case { } exception:
                SetMessageAndMarkHandled(context, exception.Message, StatusCodes.Status500InternalServerError);
                break;
        }
    }

    private void SetMessageAndMarkHandled(ExceptionContext context, string message, int statusCode)
    {
        context.HttpContext.Response.StatusCode = statusCode;

        context.Result = new ObjectResult(new
        {
            Message = message
        });

        context.ExceptionHandled = true;
    }
}