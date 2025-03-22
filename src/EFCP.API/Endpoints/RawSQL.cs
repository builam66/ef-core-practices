using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.RawSQL;

namespace EFCP.API.Endpoints
{
    public class RawSQL : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/raw-sql", async (ISender sender) =>
            {
                var result = await sender.Send(new RawSQLQuery());

                return Results.Ok(result);
            });
        }
    }
}
