using CED.Web.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Users.Queries.GetAllTutorInformationsAdvanced;
using TutorCenter.Domain;

namespace TutorCenter.Administrator.Controllers;
[Authorize(Policy = "RequireAdministratorRole")]
[Route("[controller]")]
public class TutorController : Controller
{
    private readonly ILogger<TutorController> _logger;

    private readonly ISender _mediator;
    private readonly IMapper _mapper;


    public TutorController(ILogger<TutorController> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;
    }

    private void PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
    }

    #region basic Tutor management

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetAllTutorInformationsAdvancedQuery();
        var userDtos = await _mediator.Send(query);
        if(userDtos.IsSuccess)
            return View(userDtos.Value);
        return RedirectToAction("Error", "Home");
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        PackStaticListToView();
        var query = new GetObjectQuery<TutorForDetailDto>
        {
            ObjectId = id
        };
        var result = await _mediator.Send(query);
        if(result.IsSuccess)
            return View(result.Value);
        return RedirectToAction("Error", "Home");
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TutorForDetailDto userForDetailDto, List<Guid> subjectId)
    {
        if (id != userForDetailDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var query = new CreateUpdateTutorCommand(userForDetailDto, subjectId);
                var result = await _mediator.Send(query);

                if (result.IsFailed)
                {
                    _logger.LogError("Create user failed!");
                    foreach (var v in result.Errors)
                    {
                        _logger.LogError("Error: " + v.Message);
                    }
                    return Helper.RenderRazorViewToString(
                        this,
                        "Edit",
                        userForDetailDto,
                        true
                    );
                }

                PackStaticListToView();

                return Helper.RenderRazorViewToString(
                    this,
                    "Edit",
                    userForDetailDto
                );
            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " + ex.Message +
                                             "see your system administrator.");
            }
        }

        return Helper.RenderRazorViewToString(
            this,
            "Edit",
            userForDetailDto,
            true
        );

    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        PackStaticListToView();
        return View(new TutorForDetailDto());
    }


    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TutorForDetailDto userForDetailDto, List<Guid> subjectId) // cant use userdto
    {
        userForDetailDto.LastModificationTime = DateTime.UtcNow;
        var command = new CreateUpdateTutorCommand(userForDetailDto, subjectId);
        if (!ModelState.IsValid)
        {
            return View("Create", userForDetailDto);
        }

        var result = await _mediator.Send(command);
        if (result.IsFailed)
        {
            foreach (var v in result.Errors)
            {
                _logger.LogError("{0}", v.Message);
            }
            return RedirectToAction("Error", "Home");
        }
       
            return RedirectToAction("Index");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        var query = new GetObjectQuery<TutorForDetailDto>() { ObjectId = id };
        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            return NotFound();
        }

        return Helper.RenderRazorViewToString(this, "Delete", result.Value);

    }

    [HttpPost("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new DeleteUserCommand((Guid)id);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpGet("Detail")]
    public async Task<IActionResult> Detail(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new GetObjectQuery<TutorForDetailDto>() { ObjectId = (Guid)id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return RedirectToAction("Error", "Home");
    }

    #endregion
    [HttpGet("Subjects")]
    public async Task<IActionResult> Subjects(string id)
    {
        var query = new GetObjectQuery<PaginatedList<SubjectDto>>()
        {
            ObjectId = new Guid(id)
        };
        var subjectDtos = await _mediator.Send(query);
        return Helper.RenderRazorViewToString(this, "_Subjects", subjectDtos.Value);
    }
    [HttpPost("RemoveTutorVerification")]
    public async Task<IActionResult> RemoveTutorVerification(string id)
    {
        var query = new RemoveTutorVerificationCommand(new Guid(id));
        var result = await _mediator.Send(query);
        return Json(new
        {
            res = result
        });
        
    }

}