using Carter;
using EFCP.Application.Practice.Queries;
using MediatR;

namespace EFCP.API.Endpoints
{
    public class DynamicallyConstructed : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/dynamically-constructed", async (ISender sender) =>
            {
                var result = await sender.Send(new DynamicallyConstructedQuery());

                return Results.Ok(result);
            });
        }
    }
}
