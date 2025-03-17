using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public record GetOldQuery() : IRequest<GetOldResult>;

    public record GetOldResult(long ExecutionTime);

    public class GetOldQueryHandler(IImdbDbContext _dbContext) : IRequestHandler<GetOldQuery, GetOldResult>
    {
        public async Task<GetOldResult> Handle(GetOldQuery query, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var sample = await _dbContext.Attributes.Include(a => a.TitleNames).ToListAsync(cancellationToken);

            stopwatch.Stop();

            return new GetOldResult(stopwatch.ElapsedMilliseconds);
        }
    }
}
