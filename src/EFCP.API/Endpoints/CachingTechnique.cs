using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.CachingTechnique;

namespace EFCP.API.Endpoints
{
    public class CachingTechnique : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/caching-technique", async (ISender sender) =>
            {
                var result = await sender.Send(new CachingTechniqueQuery());

                return Results.Ok(result);
            });
        }
    }
}
