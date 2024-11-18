using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Movies.Api.Auth;

public class AdminAuthRequirement : IAuthorizationHandler, IAuthorizationRequirement
{
    private readonly string _apiKey;

    public AdminAuthRequirement(string apiKey)
    {
        _apiKey = apiKey;
    }

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.User.HasClaim(AuthConstants.AdminUserPolicyName, "true"))
        {
            context.Succeed(this);
            return Task.CompletedTask;
        }
        
        var httpContext = context.Resource as HttpContext;
        if (httpContext is null)
        {
            return Task.CompletedTask;
        }

        if (!httpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var apiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (_apiKey != apiKey)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        
        var identity = (ClaimsIdentity)httpContext.User.Identity!;
        identity.AddClaim(new Claim("userid", Guid.Parse("fa06d7a4-f1b8-470d-8fb2-7e9edb32bd49").ToString()));
        context.Succeed(this);
        return Task.CompletedTask;
    }
}