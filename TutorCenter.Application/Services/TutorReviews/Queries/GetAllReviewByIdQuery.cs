using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.TutorReviews.Queries;

public class GetAllReviewByIdQuery : GetObjectQuery<PaginatedList<TutorReviewDto>>
{
    
}