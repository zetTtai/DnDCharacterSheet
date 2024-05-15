
using DnDCharacterSheet.Application.Currency.ConvertMoney;

namespace DnDCharacterSheet.Web.Endpoints;

public class Currency : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPut(ConvertMoney, "conversion", statusCodes: GetStatusCodes(StatusCodes.Status200OK, bad_request: true, unauthorized: false));
    }

    public async Task<IResult> ConvertMoney(ISender sender, ConvertMoneyCommand command)
    {
        var response = await sender.Send(command);
        return response.ToActionResult();
    }
}
