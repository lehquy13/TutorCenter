using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TutorCenter.Administrator.Controllers;

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
        return RedirectToAction("Edit", "Course", new { id = id });
    }
}