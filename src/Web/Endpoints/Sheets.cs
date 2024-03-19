using CleanArchitecture.Application.Sheets.Commands.CreateSheet;

namespace CleanArchitecture.Web.Endpoints;

public class Sheets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        _ = app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateSheet);
    }

    public Task<int> CreateSheet(ISender sender, CreateSheetCommand command)
    {
        return sender.Send(command);
    }
}
