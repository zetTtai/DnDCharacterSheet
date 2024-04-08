
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Events.Sheets;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;

public record DeleteSheetCommand(int Id) : IRequest;

public class DeleteSheetCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteSheetCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(DeleteSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id], cancellationToken);

        _context.Sheets.Remove(entity!);

        entity!.AddDomainEvent(new SheetDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
