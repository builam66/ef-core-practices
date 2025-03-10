using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public record GetOldQuery() : IRequest<GetOldResult>;

    public record GetOldResult(long ExecutionTime);

    public class GetOldQueryHandler(IAdventureWorks2022Context _dbContext) : IRequestHandler<GetOldQuery, GetOldResult>
    {
        public async Task<GetOldResult> Handle(GetOldQuery query, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var sample = await _dbContext.Addresses.Include(a => a.BusinessEntityAddresses).ToListAsync(cancellationToken);

            stopwatch.Stop();

            return new GetOldResult(stopwatch.ElapsedMilliseconds);
        }
    }
}
