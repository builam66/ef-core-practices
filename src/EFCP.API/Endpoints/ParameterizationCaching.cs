using Carter;
using EFCP.Application.Practice.Queries;
using MediatR;

namespace EFCP.API.Endpoints
{
    public class ParameterizationCaching : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/parameterization-caching", async (ISender sender) =>
            {
                var result = await sender.Send(new ParameterizationCachingQuery());

                return Results.Ok(result);
            });
        }
    }
}
