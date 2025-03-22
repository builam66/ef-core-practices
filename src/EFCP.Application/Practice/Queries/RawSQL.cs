using EFCP.Application.Abstractions;
using EFCP.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class RawSQL
    {
        public record RawSQLQuery() : IRequest<RawSQLResult>;

        public record RawSQLResult(long NormalQuery, long RawSQLQuery);

        public class RawSQLQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<RawSQLQuery, RawSQLResult>
        {
            public async Task<RawSQLResult> Handle(RawSQLQuery query, CancellationToken cancellationToken)
            {
                var stopwatch = Stopwatch.StartNew();

                var newSample = await _dbContext.TitleNames
                        .FromSql<TitleName>($"SELECT [titleId],[ordinal],[region],[language],[isOriginal],[title] FROM [imdb].[dbo].[TitleNames] WHERE [region] is NULL")
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var oldSample = await _dbContext.TitleNames
                        .Where(t => t.Region == null)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new RawSQLResult(oldTime, newTime);
            }
        }
    }
}
