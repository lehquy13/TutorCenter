using CED.Contracts;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.TutorReview;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace CED.Application.Services.TutorReviews.Queries;

public class GetAllReviewByIdQuery : GetObjectQuery<PaginatedList<TutorReviewDto>>
{
    
}