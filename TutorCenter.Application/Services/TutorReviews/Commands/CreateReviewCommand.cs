using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.TutorReview;

namespace TutorCenter.Application.Services.TutorReviews.Commands;

public class CreateReviewCommand
    : IRequest<Result<bool>>
{
    public TutorReviewDto ReviewDto { get; set; } = null!;
    public string LearnerEmail { get; set; } = null!;
    public string TutorEmail { get; set; } = null!;
    public int ClassInformationId { get; set; }

}

