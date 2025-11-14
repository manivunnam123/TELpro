using Microsoft.AspNetCore.Diagnostics;

namespace TELpro.Dependency
{
    public class Class : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var m = new 
        }
    }
}
