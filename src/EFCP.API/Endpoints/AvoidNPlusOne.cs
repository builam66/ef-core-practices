using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.AvoidNPlusOne;

namespace EFCP.API.Endpoints
{
    public class AvoidNPlusOne : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/avoid-n-plus-one", async (ISender sender) =>
            {
                var result = await sender.Send(new AvoidNPlusOneQuery());

                return Results.Ok(result);
            });
        }
    }
}
