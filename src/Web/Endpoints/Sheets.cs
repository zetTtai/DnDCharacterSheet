
using CleanArchitecture.Application;

namespace CleanArchitecture.Web.Endpoints;

public class Sheets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateSheet);
    }

    public Task<int> CreateSheet(ISender sender, CreateSheetCommand command)
    {
        return sender.Send(command);
    }
}
