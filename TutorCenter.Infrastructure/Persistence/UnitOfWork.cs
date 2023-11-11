using EduSmart.Domain.Repository;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Repository;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly AppDbContext _ceddbContext;
    public UnitOfWork(ILogger<UnitOfWork> logger, AppDbContext ceddbContext )
    {
        _logger = logger;
        _ceddbContext = ceddbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("On save changes...");
        return await _ceddbContext.SaveChangesAsync(cancellationToken);
    }
}