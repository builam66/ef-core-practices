using EFCP.Application.Abstractions;
using EFCP.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class CachingTechnique
    {
        public record CachingTechniqueQuery() : IRequest<CachingTechniqueResult>;

        public record CachingTechniqueResult(long WithoutCaching, long WithCaching);

        private static Dictionary<string, List<TitleName>> fakeCache = [];

        public class CachingTechniqueQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<CachingTechniqueQuery, CachingTechniqueResult>
        {
            public async Task<CachingTechniqueResult> Handle(CachingTechniqueQuery query, CancellationToken cancellationToken)
            {
                var stopwatch = Stopwatch.StartNew();

                var newSample = new List<TitleName>();
                if (fakeCache.TryGetValue("cacheKey", out var titleNames))
                {
                    newSample = titleNames;
                }
                else
                {
                    newSample = await _dbContext.TitleNames
                        .Where(t => t.Region == null)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                    fakeCache["cacheKey"] = newSample;
                }

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var oldSample = await _dbContext.TitleNames
                    .Where(t => t.Region == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new CachingTechniqueResult(oldTime, newTime);
            }
        }
    }
}
