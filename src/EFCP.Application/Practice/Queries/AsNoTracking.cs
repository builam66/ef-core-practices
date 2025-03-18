using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public record AsNoTrackingQuery() : IRequest<AsNoTrackingResult>;

    public record AsNoTrackingResult(long WithoutAsNoTracking, long WithAsNoTracking);

    public class AsNoTrackingQueryHandler(IImdbDbContext _dbContext)
        : IRequestHandler<AsNoTrackingQuery, AsNoTrackingResult>
    {
        public async Task<AsNoTrackingResult> Handle(AsNoTrackingQuery query, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var newSample = await _dbContext.TitleNames
                    .Where(t => t.Region == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

            stopwatch.Stop();
            var newTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();

            var oldSample = await _dbContext.TitleNames
                    .Where(t => t.Region == null)
                    .ToListAsync(cancellationToken);

            stopwatch.Stop();
            var oldTime = stopwatch.ElapsedMilliseconds;

            return new AsNoTrackingResult(oldTime, newTime);
        }
    }
}
