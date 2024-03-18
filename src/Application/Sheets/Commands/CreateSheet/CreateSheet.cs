

using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Events.Sheets;

namespace CleanArchitecture.Application;

public record CreateSheetCommand : IRequest<int>
{
    public required string CharacterName { get; set; }
}

public class CreateSheetCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSheetCommand, int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(CreateSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = new Sheet
        {
            CharacterName = request.CharacterName
        };

        entity.AddDomainEvent(new SheetCreatedEvent(entity));
        _context.Sheets.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
