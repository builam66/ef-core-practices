using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class UsePagination
    {
        public record UsePaginationQuery() : IRequest<UsePaginationResult>;

        public record UsePaginationResult(long OffsetBased, long CursorBased, string Note);

        public class UsePaginationQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<UsePaginationQuery, UsePaginationResult>
        {
            public async Task<UsePaginationResult> Handle(UsePaginationQuery query, CancellationToken cancellationToken)
            {
                var pageSize = 20;
                var stopwatch = Stopwatch.StartNew();

                var newSample1 = await _dbContext.TitleNames
                        .Take(pageSize)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                var prePageLast = newSample1.Last();
                var newSample2 = await _dbContext.TitleNames
                        .Where(t => (t.TitleId * 1000 + t.Ordinal) > (prePageLast.TitleId * 1000 + prePageLast.Ordinal)) // Using last ID of prev page
                        .Take(pageSize)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var page = 1;
                var oldSample1 = await _dbContext.TitleNames
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);
                page++;
                var oldSample2 = await _dbContext.TitleNames
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new UsePaginationResult(oldTime, newTime, "[OFFSET BASED] slow when high offset | [CURSOR BASED] cannot goto specific page");
            }
        }
    }
}
