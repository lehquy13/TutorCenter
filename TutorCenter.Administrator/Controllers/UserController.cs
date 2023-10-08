using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CED.Application.Services.Users.Admin.Commands;
using CED.Contracts;
using CED.Domain.Shared;
using CED.Web.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace CED.Web.Controllers;

[Authorize(Policy = "RequireAdministratorRole")]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private readonly ISender _mediator;
    //private readonly IMapper _mapper;


    public UserController(ILogger<UserController> logger, ISender sender)
    {
        _logger = logger;
        _mediator = sender;
       // _mapper = mapper;
    }

    private void PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
    }

    #region basic user management

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetObjectQuery<PaginatedList<UserDto>>()
        {
            PageSize = 200
        };
        var userDtos = await _mediator.Send(query);
        if(userDtos.IsSuccess)
            return View(userDtos.Value);
        return RedirectToAction("Error", "Home");
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        PackStaticListToView();
        var query = new GetObjectQuery<UserDto>()
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
    public async Task<IActionResult> Edit(Guid id, UserDto userDto)
    {
        if (id != userDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var query = new CreateUpdateUserCommand(userDto,"");


                var result = await _mediator.Send(query);

                if (result.IsFailed)
                {
                    _logger.LogError("Create user failed!");
                    return RedirectToAction("Error", "Home");
                }

                PackStaticListToView();

                return Helper.RenderRazorViewToString(
                    this,
                    "Edit",
                    userDto
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

        return View(userDto);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        PackStaticListToView();
        return View(new UserDto());
    }


    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserDto userDto) // cant use userdto
    {
        userDto.LastModificationTime = DateTime.UtcNow;
        var command = new CreateUpdateUserCommand(userDto,"");


        var result = await _mediator.Send(command);
        if (result.IsFailed)
        {
            foreach (var v in result.Errors)
            {
                _logger.LogError("Create user failed! {VMessage}", v.Message);
            }
            return RedirectToAction("Error", "Home");
        }
        {
            return RedirectToAction("Index");
        }
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = new GetObjectQuery<UserDto>() { ObjectId = (Guid)id };
        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            foreach (var v in result.Errors)
            {
                _logger.LogError("Delete user failed! {VMessage}", v.Message);
            }
            return RedirectToAction("Error", "Home");
        }

        return Helper.RenderRazorViewToString(this, "Delete", result.Value);
        // return View(result);
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

        var query = new GetObjectQuery<UserDto>() { ObjectId = (Guid)id };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return View(result.Value);
        }

        return RedirectToAction("Error", "Home");
    }

    #endregion

    [HttpGet("Student")]
    public async Task<IActionResult> Student()
    {
        var query = new GetObjectQuery<PaginatedList<LearnerDto>>
        {
            PageSize = 200
        };
        var studentDtos = await _mediator.Send(query);
        if(studentDtos.IsSuccess)
            return View(studentDtos.Value);
        return RedirectToAction("Error", "Home");
    }

  
   
}