using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenter.Administrator.Models;
using TutorCenter.Administrator.Utilities;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Services.Users.Admin.Commands.CreateUpdateUser;
using TutorCenter.Domain;

namespace TutorCenter.Administrator.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(ISender mediator, IMapper mapper, ILogger<ProfileController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        private void PackStaticListToView()
        {
            ViewData["Roles"] = EnumProvider.Roles;
            ViewData["Genders"] = EnumProvider.Genders;
            ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
        }

        [HttpGet("")]
        public async Task<IActionResult> Profile()
        {
            PackStaticListToView();

            var identity = HttpContext.User.Identities.First();

            var query = new GetObjectQuery<UserDto>()
            {
                ObjectId =Int32.Parse( identity.Claims.FirstOrDefault()?.Value )
            };

            var loginResult = await _mediator.Send(query);

            if (loginResult.IsSuccess)
            {
                var changePasswordRequest = _mapper.Map<ChangePasswordCommand>(loginResult);
                return View(new ProfileViewModel
                {
                    UserDto = loginResult.Value,
                    ChangePasswordCommand = changePasswordRequest
                });
            }

            return RedirectToAction("Index", "Authentication", new LoginQuery("", ""));
        }

        [HttpPost("ChoosePicture")]
        public async Task<IActionResult> ChoosePicture(IFormFile? formFile)
        {
            if (formFile == null)
            {
                return Json(false);
            }

            var image = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
           
            return Json(new { res = true, image = "temp\\" +  Path.GetFileName(image) });
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto userDto, IFormFile? formFile)
        {
           
            PackStaticListToView();

            if (ModelState.IsValid)
            {
                try
                {
                    var filePath = string.Empty;
                    if (formFile != null)
                    {
                        filePath = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
                    }
                    var query = new CreateUpdateUserCommand(userDto,filePath);

                    var result = await _mediator.Send(query);
                    ViewBag.Updated = result;
                    Helper.ClearTempFile(_webHostEnvironment.WebRootPath);

                    if (result.IsSuccess)
                    {
                        HttpContext.Session.SetString("name", query.UserDto.FirstName + query.UserDto.LastName);
                        HttpContext.Session.SetString("image", query.UserDto.Image);
                    }
                    return Helper.RenderRazorViewToString(this, "Profile", new ProfileViewModel
                    {
                        UserDto = userDto,
                        ChangePasswordCommand = _mapper.Map<ChangePasswordCommand>(userDto)
                    });
                }
                catch (Exception ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " + ex.Message +
                                                 "see your system administrator.");
                }
            }

            return Helper.RenderRazorViewToString(this, "_ProfileEdit",
                userDto,
                true
            );
        }

        [HttpPost("ChangePassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordRequest)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    var query = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);

                    var loginResult = await _mediator.Send(query);

                    if (loginResult.IsSuccess)
                    {
                        return Helper.RenderRazorViewToString(this, "_ChangePassword", new TutorCenter.Contracts.Authentication.ChangePasswordRequest { Id = changePasswordRequest.Id});
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

            return Helper.RenderRazorViewToString(this, "_ChangePassword", changePasswordRequest,true);

        }
    }
}