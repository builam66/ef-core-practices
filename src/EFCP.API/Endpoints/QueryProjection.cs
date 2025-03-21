using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.QueryProjection;

namespace EFCP.API.Endpoints
{
    public class QueryProjection : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/query-projection", async (ISender sender) =>
            {
                var result = await sender.Send(new QueryProjectionQuery());

                return Results.Ok(result);
            });
        }
    }
}
