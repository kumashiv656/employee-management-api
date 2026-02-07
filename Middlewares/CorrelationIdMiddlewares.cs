

namespace EmployeeApi.Middlewares;

public class CorrelationIdMiddleware
{
    
    private readonly RequestDelegate _next;
    private const string header = "X-Correlarion-Id";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }
public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[header].FirstOrDefault()
        ?? Guid.NewGuid().ToString();

        context.Items[header] = correlationId;
        context.Response.Headers[header] = correlationId;

        using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
        

    }

}