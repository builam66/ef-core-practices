using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class AvoidNPlusOne
    {
        public record AvoidNPlusOneQuery() : IRequest<AvoidNPlusOneResult>;

        public record AvoidNPlusOneResult(long NPlusOne, long AvoidNPlusOne);

        public class AvoidNPlusOneQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<AvoidNPlusOneQuery, AvoidNPlusOneResult>
        {
            public async Task<AvoidNPlusOneResult> Handle(AvoidNPlusOneQuery query, CancellationToken cancellationToken)
            {
                var stopwatch = Stopwatch.StartNew();

                var newSample = await _dbContext.Titles
                    .Include(t => t.TitleName)
                    .Take(10000)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var oldSample = await _dbContext.Titles
                    .Take(10000)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
                foreach (var sample in oldSample)
                {
                    var titleName = _dbContext.TitleNames.FirstOrDefault(t => t.TitleId == sample.TitleId);
                }

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new AvoidNPlusOneResult(oldTime, newTime);
            }
        }
    }
}
