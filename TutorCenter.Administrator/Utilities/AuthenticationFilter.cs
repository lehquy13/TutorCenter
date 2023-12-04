using TutorCenter.Application.Services.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace TutorCenter.Web.Utilities;

public class AuthenticationFilter : IAsyncActionFilter
{
    private readonly ISender _mediator;

    public AuthenticationFilter(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        if(context.HttpContext.Session.GetString("access_token") is {} token)
        {
            return;
        }
        var id = resultContext.HttpContext.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
        if(id is null) return;
        var userId = new Guid(id);
        await _mediator.Send(new UpdateTutorActivityCommand()
        {
            UserId = userId
        });
       

    }
}