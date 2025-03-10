using EFCP.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCP.Application.Practice.Queries
{
    public record GetNewQuery() : IRequest<GetNewResult>;

    public record GetNewResult(long ExecutionTime);

    public class GetNewQueryHandler(IAdventureWorks2022Context _dbContext) : IRequestHandler<GetNewQuery, GetNewResult>
    {
        public async Task<GetNewResult> Handle(GetNewQuery query, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var sample = await _dbContext.Addresses.Include(a => a.BusinessEntityAddresses).ToListAsync(cancellationToken);

            stopwatch.Stop();

            return new GetNewResult(stopwatch.ElapsedMilliseconds);
        }
    }
}
