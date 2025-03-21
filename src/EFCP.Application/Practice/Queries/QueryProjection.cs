using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class QueryProjection
    {
        public record QueryProjectionQuery() : IRequest<QueryProjectionResult>;

        public record QueryProjectionResult(long WithoutQueryProjection, long WithQueryProjection);

        public class QueryProjectionQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<QueryProjectionQuery, QueryProjectionResult>
        {
            public async Task<QueryProjectionResult> Handle(QueryProjectionQuery query, CancellationToken cancellationToken)
            {
                var stopwatch = Stopwatch.StartNew();

                var newSample = await _dbContext.TitleNames
                        .Include(t => t.Attributes)
                        .Where(t => t.Region == null)
                        .Select(t => new
                        {
                            t.TitleId,
                            t.Title,
                            t.Region,
                            t.Language,
                            t.Ordinal,
                            Attributes = t.Attributes.Select(a => new
                            {
                                a.Attribute1,
                                a.Class,
                            }),
                        })
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var oldSample = await _dbContext.TitleNames
                        .Include(t => t.Attributes)
                        .Where(t => t.Region == null)
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new QueryProjectionResult(oldTime, newTime);
            }
        }
    }
}
