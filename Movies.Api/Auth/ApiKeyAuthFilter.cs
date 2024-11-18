using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Movies.Api.Auth;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var apiKeyHeader))
        {
            context.Result = new UnauthorizedObjectResult("Missing API Key");
            return;
        }

        var apiKey = _configuration["ApiKey"]!;
        if (apiKey != apiKeyHeader)
        {
            context.Result = new UnauthorizedObjectResult("Invalid API Key");
        }
    }
}