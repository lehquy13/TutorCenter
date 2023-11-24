using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetTutorById;

public class GetTutorByIdQueryHandler : IRequestHandler<GetTutorByIdQuery, Result<TutorForDetailDto>>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IMapper _mapper;

    public GetTutorByIdQueryHandler(
        ITutorRepository tutorRepository,
        IMapper mapper)
    {
        _tutorRepository = tutorRepository;
        _mapper = mapper;
    }

    public async Task<Result<TutorForDetailDto>> Handle(GetTutorByIdQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var tutor = await _tutorRepository.GetById(query.ObjectId);
            if (tutor is null) return Result.Fail(new NonExistTutorError());
            var result = _mapper.Map<TutorForDetailDto>(tutor);

            // var tutorReviewDtos = result
            //     .RequestGettingClassForListDtos
            //     .Where(x => x.RequestStatus == RequestStatus.Success && x.ClassInformationDto.TutorReviewDto != null)
            //     .Select(x => x.ClassInformationDto.TutorReviewDto)
            //     .ToList();

            // result.TutorReviewDtos  = PaginatedList<TutorReviewDto>.CreateAsync(
            //     tutorReviewDtos!
            //     ,query.PageIndex,query.PageSize,tutorReviewDtos.Count
            // );
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}