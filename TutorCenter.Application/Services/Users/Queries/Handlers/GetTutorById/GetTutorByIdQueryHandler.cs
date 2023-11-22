using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetTutorById;

public class GetTutorByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<TutorForDetailDto>, TutorForDetailDto>
{
    private readonly ITutorRepository _tutorRepository;

    public GetTutorByIdQueryHandler(
        ITutorRepository tutorRepository,
        IMapper mapper
    ) : base(mapper)
    {
        _tutorRepository = tutorRepository;
    }

    public override async Task<Result<TutorForDetailDto>> Handle(GetObjectQuery<TutorForDetailDto> query,
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