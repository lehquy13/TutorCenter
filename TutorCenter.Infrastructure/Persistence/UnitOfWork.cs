using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Repository;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly AppDbContext _appDbContext;
    public UnitOfWork(ILogger<UnitOfWork> logger, AppDbContext appDbContext )
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("On save changes...");
        return await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}