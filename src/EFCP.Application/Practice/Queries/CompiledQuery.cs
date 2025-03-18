using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public record CompiledQueryQuery() : IRequest<CompiledQueryResult>;

    public record CompiledQueryResult(long WithoutCompiledQuery, long WithCompiledQuery);

    public class CompiledQueryQueryHandler(IImdbDbContext _dbContext)
        : IRequestHandler<CompiledQueryQuery, CompiledQueryResult>
    {
        public async Task<CompiledQueryResult> Handle(CompiledQueryQuery query, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var newSample = await _dbContext.SumOrdinalTitleNameByRegionAsync(null);

            stopwatch.Stop();
            var newTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();

            var oldSample = 0;
            await foreach (var titleName in _dbContext.TitleNames.Where(b => b.Region == null).AsAsyncEnumerable())
            {
                oldSample += titleName.Ordinal;
            }

            stopwatch.Stop();
            var oldTime = stopwatch.ElapsedMilliseconds;

            return new CompiledQueryResult(oldTime, newTime);
        }
    }
}
