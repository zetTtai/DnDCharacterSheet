using CleanArchitecture.Application.TodoItems.EventHandlers;
using CleanArchitecture.Domain.Events.Sheets;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Sheets.EventHandlers;
public class SheetCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger) : INotificationHandler<SheetCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger = logger;
    public Task Handle(SheetCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DnDCharacterSheet Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
