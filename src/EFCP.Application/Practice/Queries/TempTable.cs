using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public class TempTable
    {
        public record TempTableQuery() : IRequest<TempTableResult>;

        public record TempTableResult(long WithoutTempTable, long WithTempTable);

        public class TempTableQueryHandler(IImdbDbContext _dbContext)
            : IRequestHandler<TempTableQuery, TempTableResult>
        {
            public async Task<TempTableResult> Handle(TempTableQuery query, CancellationToken cancellationToken)
            {
                var random = new Random();
                var randomQueryIds = new HashSet<int>();
                while (randomQueryIds.Count < 9999999)
                {
                    int number = random.Next(1, 10000000);
                    randomQueryIds.Add(number);
                }
                var queryIds = randomQueryIds.ToList();

                var stopwatch = Stopwatch.StartNew();

                var newSample = await _dbContext.QueryLargeIdsAsync(queryIds, async (isUseTempTable) =>
                {
                    var tempQueryAble = isUseTempTable ? _dbContext.TempTable : queryIds.AsEnumerable();
                    return await _dbContext.TitleNames
                        .Where(t => tempQueryAble.Contains(t.TitleId))
                        .Select(t => new
                        {
                            t.TitleId,
                            t.Ordinal,
                            t.Region,
                            t.Language,
                            t.IsOriginal,
                            t.Title,
                        })
                        .ToListAsync(cancellationToken);
                });

                stopwatch.Stop();
                var newTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();

                var oldSample = await _dbContext.TitleNames
                        .Where(t => queryIds.Contains(t.TitleId))
                        .Select(t => new
                        {
                            t.TitleId,
                            t.Ordinal,
                            t.Region,
                            t.Language,
                            t.IsOriginal,
                            t.Title,
                        })
                        .ToListAsync(cancellationToken);

                stopwatch.Stop();
                var oldTime = stopwatch.ElapsedMilliseconds;

                return new TempTableResult(oldTime, newTime);
            }
        }
    }
}
