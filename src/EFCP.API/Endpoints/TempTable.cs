using Carter;
using MediatR;
using static EFCP.Application.Practice.Queries.TempTable;

namespace EFCP.API.Endpoints
{
    public class TempTable : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var getSampleGroup = app.MapGroup("/sample").WithTags("Sample");

            getSampleGroup.MapGet("/temp-table", async (ISender sender) =>
            {
                var result = await sender.Send(new TempTableQuery());

                return Results.Ok(result);
            });
        }
    }
}
