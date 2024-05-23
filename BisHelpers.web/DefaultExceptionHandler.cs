namespace BisHelpers.web;

public class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsJsonAsync(new ErrorDto(httpContext)
        {
            StatusCode = 500,
            Errors = [new ErrorBody {
                Message = $"Internal Server Error: {exception.GetType().Name}",
                Details = [string.Join(" ,", exception.Message, exception.InnerException?.Message, exception.InnerException?.InnerException?.Message)],
            }]
        });

        return true;
    }
}
