using DnDCharacterSheet.Domain.Events.Sheets;
using Microsoft.Extensions.Logging;

namespace DnDCharacterSheet.Application.Sheets.EventHandlers;
public class SheetDeletedEventHandler(ILogger<SheetDeletedEventHandler> logger) : INotificationHandler<SheetDeletedEvent>
{
    private readonly ILogger<SheetDeletedEventHandler> _logger = logger;
    public Task Handle(SheetDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DnDCharacterSheet Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
