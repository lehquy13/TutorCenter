using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tutor.Web.Controllers;

/// <summary>
/// A navigate controller
/// </summary>
[Route("[controller]")]
[Authorize(Policy = "RequireAdministratorRole")]

public class RequestGettingClassController : Controller
{
    [Route("Detail")]
    public IActionResult Detail(string? id)
    {
        return RedirectToAction("Edit", "ClassInformation", new { id = id });
    }
}