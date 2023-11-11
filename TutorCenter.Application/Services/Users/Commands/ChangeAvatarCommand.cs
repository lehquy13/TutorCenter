using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TutorCenter.Application.Services.Users.Commands;

public record ChangeAvatarCommand
(
    int Id,
    IFormFile? File
    ) : IRequest<Result<string>>;

