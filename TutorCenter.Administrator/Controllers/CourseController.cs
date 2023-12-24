using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenter.Administrator.Utilities;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Courses.Commands;
using TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;
using TutorCenter.Application.Services.Courses.Queries.GetCoureRequestById;
using TutorCenter.Application.Services.Courses.Queries.GetCourseQuery;
using TutorCenter.Application.Services.Subjects.Queries;
using TutorCenter.Application.Services.Users.Admin.Commands.RemoveTutorReview;
using TutorCenter.Application.Services.Users.Queries.GetAllTutorInformationsAdvanced;
using TutorCenter.Application.Services.Users.Queries.GetLearners;
using TutorCenter.Domain;

namespace TutorCenter.Administrator.Controllers;

[Route("[controller]")]
[Authorize]
public class CourseController : Controller
{
    private readonly ILogger<CourseController> _logger;

    //dependencies 
    private readonly ISender _mediator;
    private readonly IMapper _mapper;


    public CourseController(ILogger<CourseController> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;
    }

    private async Task PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
        ViewData["LearningModes"] = EnumProvider.LearningModes;
        ViewData["Statuses"] = EnumProvider.Status;


        var subjects = await _mediator.Send(new GetAllSubjectsQuery());
        ViewData["Subjects"] = subjects.Value;
    }

    private async Task PackStudentAndTuTorList()
    {
        var tutorDtos = await _mediator.Send(new GetAllTutorInformationsAdvancedQuery());
        var studentDtos = await _mediator.Send(new GetLearnersQuery());
        ViewData["TutorDtos"] = tutorDtos;
        ViewData["StudentDtos"] = studentDtos;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index(string? type)
    {
        //var query = new GetObjectQuery<List<CourseDto>>();
        var query = new GetAllCoursesQuery()
        {
            Filter = type ?? ""
        };
        var courses = await _mediator.Send(query);
        if (courses.IsSuccess)
            return View(courses.Value);
        return View(new List<CourseForListDto>());
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        await PackStaticListToView();

        await PackStudentAndTuTorList();

        var query = new GetCourseQuery()
        {
            Id = id
        };
        var result = await _mediator.Send(query);
        ViewBag.Action = "Edit";

        if (result.IsSuccess)
        {
            var classDtoViewModel = _mapper.Map<CourseForDetailDto>(result.Value);
            return View(classDtoViewModel);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseForDetailDto classDto)
    {
        if (id != classDto.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(classDto);
        }

        var query = new CreateUpdateCourseCommand()
        {
            CourseDto = classDto
        };

        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            return View(classDto);
        }

        await PackStaticListToView();
        await PackStudentAndTuTorList();

        return Helper.RenderRazorViewToString(
            this,
            "Edit",
            classDto
        );
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await PackStaticListToView();
        //await PackStudentAndTuTorList();

        return View(new CourseForDetailDto());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseForDetailDto classDto)
    {
        classDto.LastModificationTime = DateTime.UtcNow;
        var query = new CreateUpdateCourseCommand() { CourseDto = classDto };
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

        var query = new GetCourseQuery() { Id = (int)id };
        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            return NotFound();
        }

        return
            Helper.RenderRazorViewToString(this, "Delete", result.Value);
    }

    [HttpPost("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var query = new DeleteCourseCommand((int)id);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
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

        var query = new GetCourseQuery() { Id = (int)id };

        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpGet]
    [Route("PickTutor")]
    public async Task<IActionResult> PickTutor()
    {
        var query = new GetAllTutorInformationsAdvancedQuery();
        var userDtos = await _mediator.Send(query);
        return Helper.RenderRazorViewToString(this, "PickTutor", userDtos.Value);
    }

    [HttpGet("ViewTutor")]
    public async Task<IActionResult> ViewTutor(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var query = new GetCourseQuery() { Id = (int)id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Helper.RenderRazorViewToString(this, "ViewTutor", result.Value);
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpGet("ViewReview")]
    public async Task<IActionResult> ViewReview(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var query = new GetCourseQuery(){ Id = (int)id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            TempData["ClassId"] = id.ToString();
            return Helper.RenderRazorViewToString(this, "_TutorReview", result);
        }

        return Helper.RenderRazorViewToString(this, "", result, false);
    }

    [HttpPost("Choose")]
    public IActionResult Choose(int? tutorId)
    {
        if (tutorId == null || tutorId == 0)
        {
            return NotFound();
        }

        return Json(new { tutorId = tutorId });
    }

    [HttpPost("RemoveReview")]
    public async Task<IActionResult> RemoveReview(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var result = await _mediator.Send(new RemoveTutorReviewCommand(id));
        if (TempData["ClassId"] != null && result.IsSuccess)
        {
            int guid = (int)(TempData["ClassId"] ?? 0);
            return RedirectToAction("Edit", new { id = guid });
        }

        return NotFound();
    }

    [HttpGet("EditRequest")]
    public async Task<IActionResult> EditRequest(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var result = await _mediator
            .Send(
                new GetCourseRequestByIdQuery()
                {
                    Id = id
                }
            );

        return Helper.RenderRazorViewToString(this, "_EditRequest", result.Value);
    }

    [HttpPost("CancelRequest")]
    public async Task<IActionResult> CancelRequest(RequestGettingClassMinimalDto requestGettingClassMinimalDto)
    {
        var result = await _mediator
            .Send(
                new CancelRequestGettingCourseCommand(requestGettingClassMinimalDto)
            );

        return RedirectToAction("Edit", new { id = requestGettingClassMinimalDto.CourseId });
    }
}