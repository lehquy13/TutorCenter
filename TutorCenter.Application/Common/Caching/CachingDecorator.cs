using LazyCache;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TutorCenter.Application.Common.Caching;

public class CachingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>, new()
{
    private readonly IAppCache _cache;
    private readonly ILogger<TResponse> _logger;
    public CachingBehavior(IAppCache cache, ILogger<TResponse> logger)
    {
        _cache = cache;
        _logger = logger;
        //_settings = settings.Value;
    }
   

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var defaultRequestKey = GenerateCacheKey(new TRequest());
        string key = GenerateCacheKey(request);
        _logger.LogInformation($"Generated key: {key}");
        if (defaultRequestKey.Equals(key))
        {
            return _cache.GetOrAddAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await next();
            });
        }
        return next();
    }
    private string GenerateCacheKey(TRequest request)
    {
        // Implement your logic to generate a unique cache key based on the request
        // You can concatenate request properties or serialize the request object, depending on your requirements
        // Return a string representing the cache key

        return request.GetType() + JsonConvert.SerializeObject(request);
        //return request.GetType().AssemblyQualifiedName + JsonConvert.SerializeObject(request);
    }
}