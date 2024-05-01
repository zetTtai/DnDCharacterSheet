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
    private readonly ICurrencyService _currencyService = currencyService;

    public Task<Response<Money>> Handle(ConvertMoneyCommand request, CancellationToken cancellationToken)
    {
        var quantityToConvert = request.CurrentMoney.GetByCurrency(request.SrcCurrency);
        if (quantityToConvert < request.Quantity)
        {
            return Task.FromResult(Response<Money>.Failure(HttpStatusCode.BadRequest, 
                [$"{request.Quantity} is not a valid Quantity (Current ammount of {request.SrcCurrency} is {quantityToConvert} )"]));
        }
        try
        {
            var money = _currencyService.Convert(request.CurrentMoney, request.SrcCurrency, request.DstCurrency, request.Quantity);
            return Task.FromResult(Response<Money>.Success(money));

        } catch (InvalidOperationException ex)
        {
            return Task.FromResult(Response<Money>.Failure(HttpStatusCode.BadRequest, [ex.Message]));
        }

    }
}
