using Carter;
using EFCP.Application.Practice.Queries;
using MediatR;

namespace EFCP.API.Endpoints
{
    public class AsNoTracking : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/asnotracking", async (ISender sender) =>
            {
                var result = await sender.Send(new AsNoTrackingQuery());

                return Results.Ok(result);
            });
        }
    }
}
