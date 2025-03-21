using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.UsePagination;

namespace EFCP.API.Endpoints
{
    public class UsePagination : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/use-pagination", async (ISender sender) =>
            {
                var result = await sender.Send(new UsePaginationQuery());

                return Results.Ok(result);
            });
        }
    }
}
