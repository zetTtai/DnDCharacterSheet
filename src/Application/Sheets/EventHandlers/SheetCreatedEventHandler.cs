using DnDCharacterSheet.Domain.Events.Sheets;
using Microsoft.Extensions.Logging;

namespace DnDCharacterSheet.Application.Sheets.EventHandlers;
internal class SheetCreatedEventHandler(ILogger<SheetCreatedEventHandler> logger) : INotificationHandler<SheetCreatedEvent>
{
    private readonly ILogger<SheetCreatedEventHandler> _logger = logger;
    public Task Handle(SheetCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DnDCharacterSheet Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
