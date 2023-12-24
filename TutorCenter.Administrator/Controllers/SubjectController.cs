using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenter.Administrator.Utilities;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Subjects;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Subjects.Commands;
using TutorCenter.Application.Services.Subjects.Queries;

namespace TutorCenter.Administrator.Controllers;

//[Authorize(Policy = "RequireAdministratorRole")]
[Route("[controller]")]
public class SubjectController : Controller
{
    private readonly ILogger<SubjectController> _logger;
    //dependencies 
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public SubjectController(ILogger<SubjectController> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetAllSubjectsQuery();
        var subjectDtos = await _mediator.Send(query);

        return View(subjectDtos.Value);
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(int id)
    {

        var query = new GetSubjectQuery()
        {
            Id = id
        };
        var result = await _mediator.Send(query);

        return View(result.Value);
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SubjectDto subjectDto)
    {
        if (id != subjectDto.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                var query = new CreateUpdateSubjectCommand()
                {
                    SubjectDto = subjectDto
                };
                var result = await _mediator.Send(query);
                ViewBag.Updated = true;
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index","Subject");
                }
            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " + ex.Message +
                                             "see your system administrator.");
            }
        }
        return View(subjectDto);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new SubjectDto());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SubjectDto subjectDto) 
    {
        subjectDto.LastModificationTime = DateTime.UtcNow;
        var query = new CreateUpdateSubjectCommand() { SubjectDto = subjectDto };
        var result = await _mediator.Send(query);

        return RedirectToAction("Index");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = new GetObjectQuery<SubjectDto>() { ObjectId = (int)id };
        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            return NotFound();
        }

        return Helper.RenderRazorViewToString(this, "Delete", result);
    }

    [HttpGet("DeleteConfirmed/{id}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var query = new DeleteSubjectCommand(id);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index", "Home");

        }
        return RedirectToAction("Error", "Home");
    }

    [HttpGet("Detail")]
    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var query = new GetObjectQuery<SubjectDto>() { ObjectId = (int)id };

        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return View(result.Value);

        }
        return RedirectToAction("Error", "Home");
    }
}