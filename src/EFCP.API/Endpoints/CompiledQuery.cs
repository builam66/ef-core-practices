using Carter;
using EFCP.Application.Practice.Queries;
using MediatR;

namespace EFCP.API.Endpoints
{
    public class CompiledQuery : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/compiled-query", async (ISender sender) =>
            {
                var result = await sender.Send(new CompiledQueryQuery());

                return Results.Ok(result);
            });
        }
    }
}
