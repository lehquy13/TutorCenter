using FluentResults;
using MapsterMapper;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Review;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.TutorReviews.Queries;

public class GetAllReviewByIdQueryHandler : PaginatedList<TutorReviewDto>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IRepository<TutorReview> _tutorReviewRepository;
    private readonly IMapper _mapper;

    public GetAllReviewByIdQueryHandler(ITutorRepository tutorRepository, IRepository<TutorReview> tutorReviewRepository, IMapper mapper, ICourseRepository courseRepository)
    {
        _tutorRepository = tutorRepository;
        _tutorReviewRepository = tutorReviewRepository;
        this._mapper = mapper;
        _courseRepository = courseRepository;
    }

    public async Task<Result<PaginatedList<TutorReviewDto>>> Handle(GetAllReviewByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var resultsFromDb = await _tutorRepository.GetReviewsOfTutor(query.ObjectId);
            var resultDtos = _mapper.Map<List<TutorReviewDto>>(resultsFromDb);
            
            //testing mapping paginatedlist 
            return PaginatedList<TutorReviewDto>.CreateAsync(resultDtos,query.PageIndex,query.PageSize,resultDtos.Count);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}

