using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace TutorCenter.Web.Utilities;

public class LogUserActivity : IAsyncActionFilter
{
    private readonly ISender _mediator;

    public LogUserActivity(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        var id = resultContext.HttpContext.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
        if(id is null) return;
        var userId = new Guid(id);
        await _mediator.Send(new UpdateTutorActivityCommand()
        {
            UserId = userId
        });
       

    }
}