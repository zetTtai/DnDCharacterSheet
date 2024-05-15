using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Application.Currency.ConvertMoney;
public class ConvertMoneyCommand : IRequest<Response<Money>>
{
    public required Money CurrentMoney { get; set; }
    public Currencies SrcCurrency { get; set; }
    public Currencies DstCurrency { get; set; }
    public int Quantity { get; set; }
}

public class ConvertMoneyCommandHandler(ICurrencyService currencyService) : IRequestHandler<ConvertMoneyCommand, Response<Money>>
{
    public Task<Response<Money>> Handle(ConvertMoneyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var srcQuantity = request.CurrentMoney.GetByCurrency(request.SrcCurrency);
            if (srcQuantity < request.Quantity)
            {
                return Task.FromResult(Response<Money>.Failure(HttpStatusCode.BadRequest, 
                    [$"{request.Quantity} is not a valid Quantity (Current ammount of {request.SrcCurrency} is {srcQuantity} )"]));
            }

            var money = currencyService.Convert(request.CurrentMoney, request.SrcCurrency, request.DstCurrency, request.Quantity);
            return Task.FromResult(Response<Money>.Success(money));

        } catch (InvalidOperationException ex)
        {
            return Task.FromResult(Response<Money>.Failure(HttpStatusCode.BadRequest, [ex.Message]));
        }

    }
}
