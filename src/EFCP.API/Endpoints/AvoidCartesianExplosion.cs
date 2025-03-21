using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.AvoidCartesianExplosion;

namespace EFCP.API.Endpoints
{
    public class AvoidCartesianExplosion : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/avoid-cartesian-explosion", async (ISender sender) =>
            {
                var result = await sender.Send(new AvoidCartesianExplosionQuery());

                return Results.Ok(result);
            });
        }
    }
}
