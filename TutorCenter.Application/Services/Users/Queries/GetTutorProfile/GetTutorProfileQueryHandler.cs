using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.GetTutorProfile;

public class
    GetTutorProfileQueryHandler : IRequestHandler<GetTutorProfileQuery, Result<TutorProfileDto>>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IMapper _mapper;

    public GetTutorProfileQueryHandler(
        ITutorRepository tutorRepository,
        IMapper mapper)
    {
        _tutorRepository = tutorRepository;
        _mapper = mapper;
    }

    public async Task<Result<TutorProfileDto>> Handle(
        GetTutorProfileQuery query, CancellationToken cancellationToken)
    {
        //This task need to pull tutor data: basic information, class information, verification information, major information
        try
        {
            var tutor = await _tutorRepository.GetById(query
                .id); // This query includes verification information, major information, requests getting class
            if (tutor is null) return Result.Fail(new NonExistTutorError());

            return _mapper.Map<TutorProfileDto>(tutor);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}