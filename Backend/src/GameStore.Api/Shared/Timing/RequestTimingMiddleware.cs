using System;
using System.Diagnostics;

namespace GameStore.Api.Shared.Timing;

public class RequestTimingMiddleware(RequestDelegate next,ILogger<RequestTimingMiddleware> logger)
{
public async Task InvokeAsync(HttpContext context)
{

var stopWatch = new Stopwatch();
try{
stopWatch.Start();

await next(context);


}
finally
{
stopWatch.Stop();

var ellapsedMilliSeconds = stopWatch.ElapsedMilliseconds;

logger.LogInformation("{RequestMethod} {RequestPath} Completed with status {Status} in {ElapsedMilliSeconds} ms ",
context.Request.Method,
context.Request.Path,
context.Response.StatusCode,
ellapsedMilliSeconds);

}
}
}
