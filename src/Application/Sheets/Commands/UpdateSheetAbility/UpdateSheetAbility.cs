using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Sheets.Commands.UpdateSheetAbility;
public class UpdateSheetAbilityCommand : IRequest
{
    public required int SheetId { get; set; }
    public required int Value { get; set; }
    public required CharacterAbilities Ability { get; set; }
    public required MethodsToIncreaseAbilities Method { get; set; }
}

public class UpdateSheetAbilityCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSheetAbilityCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(UpdateSheetAbilityCommand request, CancellationToken cancellationToken)
    {
        Sheet? entity = await _context.Sheets
            .FindAsync(new object[] { request.SheetId }, cancellationToken);

        _ = Guard.Against.NotFound(request.SheetId, entity);

        // TODO: Call to services that implement this logic?

        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
