using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class AvoidCartesianExplosion
    {
        public record AvoidCartesianExplosionQuery() : IRequest<AvoidCartesianExplosionResult>;

        public record AvoidCartesianExplosionResult(long CartesianExplosion, long AvoidCartesianExplosion);

        public class AvoidCartesianExplosionQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<AvoidCartesianExplosionQuery, AvoidCartesianExplosionResult>
        {
            public async Task<AvoidCartesianExplosionResult> Handle(AvoidCartesianExplosionQuery query, CancellationToken cancellationToken)
            {
                var stopwatch = Stopwatch.StartNew();

                var newSample = await _dbContext.TitleNames
                        .Include(t => t.Attributes.Where(a => a.Attribute1.Contains("3-D")))
                        .Where(t => t.Region == null)
                        .AsSplitQuery()
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var oldSample = await _dbContext.TitleNames
                        .Include(t => t.Attributes)
                        .Where(t => t.Region == null)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new AvoidCartesianExplosionResult(oldTime, newTime);
            }
        }
    }
}
