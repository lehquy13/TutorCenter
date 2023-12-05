using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Application.Services.Authentication.Admin.Commands.ForgotPassword;

namespace TutorCenter.Administrator.Controllers;

[Route("[controller]")]
[Route("")]
public class AuthenticationController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ISender mediator, IMapper mapper, ILogger<AuthenticationController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [Route("")]
    public async Task<IActionResult> Index(string? returnUrl)
    {
        TempData["ReturnUrl"] = returnUrl;
        string? validateToken = Request.Headers[HeaderNames.Authorization];
        if (validateToken is null)
        {
            return View("Login", new LoginQuery("", ""));
        }
        var query = new ValidateTokenQuery(validateToken);

        var loginResult = await _mediator.Send(query);

        if ((bool)loginResult)
        {
            return RedirectToAction("Index", "Home");
        }

        return View("Login", new LoginQuery("", ""));
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginQuery request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var loginResult = await _mediator.Send(query);


        if (loginResult.IsSuccess is false || loginResult.User is null)
        {
            ViewBag.isFail = true;
            return View("Login", new LoginQuery("", ""));
        }
        // Store the JWT token in a cookie
      
        //store token into session
        HttpContext.Session.SetString("access_token", loginResult.Token);
        HttpContext.Session.SetString("name", loginResult.User.FullName);
        HttpContext.Session.SetString("image", loginResult.User.Image);

        var returnUrl = TempData["ReturnUrl"] as string;
        if (returnUrl is null)
            return RedirectToAction("Index", "Home");

        return Redirect(returnUrl);
    }


    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
     
        HttpContext.Session.Clear();

        return View("Login", new LoginQuery("", ""));
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var query = new ForgotPasswordCommand(email);

        var loginResult = await _mediator.Send(query);

        return RedirectToAction("Index", "Home");
    }
}
