using FluentResults;
using MapsterMapper;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;

namespace TutorCenter.Application.Services.TutorReviews.Queries;

public class GetReviewByClassIdQueryHandler : PaginatedList<TutorReviewDto>
{
  
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetReviewByClassIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        this._mapper = mapper;
    }
    public async Task<Result<TutorReviewDto>> Handle(GetObjectQuery<TutorReviewDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            if (query.ObjectId ==0)
            {
                return Result.Fail("Review not found");
            }
            var review = await _courseRepository.GetReviewByClassId(query.ObjectId);
            if (review is null)
            {
                return Result.Fail("Review not found");
            }
            return _mapper.Map<TutorReviewDto>(review);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

