using DnDCharacterSheet.Application.Common.Interfaces;

namespace DnDCharacterSheet.Application;

public class UpdateSheetCommand : IRequest
{
    private int _id;
    public required string CharacterName { get; set; }

    public void Id(int id) => _id = id;
    public int Id() => _id;
}

public class UpdateSheetCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSheetCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(UpdateSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id()], cancellationToken: cancellationToken);

        entity!.CharacterName = request.CharacterName;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
