using Carter;
using EFCP.Application.Practice.Queries;
using MediatR;

namespace EFCP.API.Endpoints
{
    public class GetSample : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("GetSample");

            getSampleGroup.MapGet("/old", async (ISender sender) =>
            {
                var result = await sender.Send(new GetOldQuery());

                return Results.Ok(result);
            });


            getSampleGroup.MapGet("/new", async (ISender sender) =>
            {
                var result = await sender.Send(new GetNewQuery());

                return Results.Ok(result);
            });
        }
    }
}
