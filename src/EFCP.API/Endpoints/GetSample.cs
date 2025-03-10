using Carter;
using EFCP.Application.Practice.Queries;
using MediatR;

namespace EFCP.API.Endpoints
{
    public class GetSample : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/sample/old", async (ISender sender) =>
            {
                var result = await sender.Send(new GetOldQuery());

                return Results.Ok(result);
            });

            app.MapGet("/sample/new", async (ISender sender) =>
            {
                var result = await sender.Send(new GetNewQuery());

                return Results.Ok(result);
            });
        }
    }
}
