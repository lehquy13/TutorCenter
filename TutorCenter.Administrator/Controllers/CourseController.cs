using CED.Web.Utilities;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;
using TutorCenter.Application.Services.Users.Queries.GetAllTutorInformationsAdvanced;
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


        var subjects = await _mediator.Send(new GetObjectQuery<PaginatedList<SubjectDto>>());
        ViewData["Subjects"] = subjects.Value;
    }

    private async Task PackStudentAndTuTorList()
    {
        var tutorDtos = await _mediator.Send(new GetAllTutorInformationsAdvancedQuery());
        var studentDtos = await _mediator.Send(new GetObjectQuery<PaginatedList<LearnerDto>>());
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
            Filter = type??""
        };
        var courses = await _mediator.Send(query);
        if(courses.IsSuccess)
            return View(courses.Value);
        return View(new List<CourseForListDto>());
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        await PackStaticListToView();

        await PackStudentAndTuTorList();

        var query = new GetObjectQuery<CourseForDetailDto>
        {
            ObjectId = id
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
    public async Task<IActionResult> Edit(Guid Id, CourseForEditDto classDto)
    {
        if (Id != classDto.Id)
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

        return Helper.RenderRazorViewToString(this, "Edit", classDto);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await PackStaticListToView();
        //await PackStudentAndTuTorList();

        return View(new CourseForEditDto());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseForEditDto classDto)
    {
        classDto.LastModificationTime = DateTime.UtcNow;
        var query = new CreateUpdateCourseCommand() { CourseDto = classDto };
        var result = await _mediator.Send(query);

        return RedirectToAction("Index");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = new GetObjectQuery<CourseForDetailDto>() { ObjectId = (Guid)id };
        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            return NotFound();
        }

        return
            Helper.RenderRazorViewToString(this, "Delete", result);
    }

    [HttpPost("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new DeleteCourseCommand((Guid)id);
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

        var query = new GetObjectQuery<CourseForDetailDto> { ObjectId = (Guid)id };

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
    public async Task<IActionResult> ViewTutor(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new GetObjectQuery<TutorForDetailDto>() { ObjectId = (Guid)id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Helper.RenderRazorViewToString(this, "ViewTutor", result.Value);
        }

        return RedirectToAction("Error", "Home");
    }
    [HttpGet("ViewReview")]
    public async Task<IActionResult> ViewReview(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new GetObjectQuery<TutorReviewDto>() { ObjectId = (Guid)id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            TempData["ClassId"] = id.ToString();
            return Helper.RenderRazorViewToString(this, "_TutorReview", result);
        }

        return Helper.RenderRazorViewToString(this, "", result, false);
    }

    [HttpPost("Choose")]
    public IActionResult Choose(Guid? tutorId)
    {
        if (tutorId == null || tutorId.Equals(Guid.Empty))
        {
            return NotFound();
        }

        return Json(new { tutorId = tutorId });
    }
    [HttpPost("RemoveReview")]
    public async Task<IActionResult> RemoveReview(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var result = await _mediator.Send(new RemoveTutorReviewCommand(id));
        if (TempData["ClassId"] != null && result.IsSuccess )
        {
            Guid guid = (Guid)(TempData["ClassId"]??"");
            return RedirectToAction("Edit", new {id = guid});
        }

        return NotFound();
    }

    [HttpGet("EditRequest")]
    public async Task<IActionResult> EditRequest(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var result = await _mediator
            .Send(
                new GetObjectQuery<Result<RequestGettingClassMinimalDto>>
                {
                    ObjectId = id
                }
            );

        return Helper.RenderRazorViewToString(this,"_EditRequest", result.Value );
    }
    [HttpPost("CancelRequest")]
    public async Task<IActionResult> CancelRequest(RequestGettingClassMinimalDto requestGettingClassMinimalDto)
    {
        var result = await _mediator
            .Send(
                new CancelRequestGettingClassCommand(requestGettingClassMinimalDto)
                
            );

        return RedirectToAction("Edit", new {id = requestGettingClassMinimalDto.CourseId});
    }
}