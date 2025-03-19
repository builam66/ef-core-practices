using EFCP.Application.Abstractions;
using EFCP.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace EFCP.Application.Practice.Queries
{
    public record DynamicallyConstructedQuery() : IRequest<DynamicallyConstructedResult>;

    public record DynamicallyConstructedResult(
        long ExpressionApiWithConstant,
        long ExpressionApiWithParameter,
        long SimpleWithParameter);

    public class DynamicallyConstructedQueryHandler(IImdbDbContext _dbContext)
        : IRequestHandler<DynamicallyConstructedQuery, DynamicallyConstructedResult>
    {
        public async Task<DynamicallyConstructedResult> Handle(DynamicallyConstructedQuery query, CancellationToken cancellationToken)
        {
            var region = "FR";
            Expression<Func<TitleName, bool>> whereLambda = b => b.Region == region;

            var stopwatch = Stopwatch.StartNew();

            var newSample1 = await _dbContext.TitleNames
                    .Where(whereLambda)
                    .CountAsync(cancellationToken);

            stopwatch.Stop();
            var newTime1 = stopwatch.ElapsedMilliseconds;

            var blogParam = Expression.Parameter(typeof(TitleName), "n");
            Expression<Func<string?>> urlParameterLambda = () => region;
            var urlParamExpression = urlParameterLambda.Body;
            whereLambda = Expression.Lambda<Func<TitleName, bool>>(
                Expression.Equal(
                    Expression.MakeMemberAccess(
                        blogParam,
                        typeof(TitleName).GetMember(nameof(TitleName.Region)).Single()),
                    urlParamExpression),
                blogParam);

            stopwatch.Restart();

            var newSample2 = await _dbContext.TitleNames
                    .Where(whereLambda)
                    .CountAsync(cancellationToken);

            stopwatch.Stop();
            var newTime2 = stopwatch.ElapsedMilliseconds;

            whereLambda = Expression.Lambda<Func<TitleName, bool>>(
                Expression.Equal(
                    Expression.MakeMemberAccess(
                        blogParam,
                        typeof(TitleName).GetMember(nameof(TitleName.Region)).Single()),
                    Expression.Constant(region)),
                blogParam);

            stopwatch.Restart();

            var oldSample = await _dbContext.TitleNames
                    .Where(whereLambda)
                    .CountAsync(cancellationToken);

            stopwatch.Stop();
            var oldTime = stopwatch.ElapsedMilliseconds;

            return new DynamicallyConstructedResult(oldTime, newTime2, newTime1);
        }
    }
}
