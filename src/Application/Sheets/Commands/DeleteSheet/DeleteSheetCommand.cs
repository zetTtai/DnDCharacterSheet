
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.Events.Sheets;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;

public record DeleteSheetCommand(int Id) : IRequest<Result>;

public class DeleteSheetCommandHandler(
    IApplicationDbContext context,
    IIdentityService identityService,
    IUser user) : IRequestHandler<DeleteSheetCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IIdentityService _identityService = identityService;
    private readonly IUser _user = user;


    public async Task<Result> Handle(DeleteSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id], cancellationToken);

        if (entity is null)
        {
            return Result.Failure([]);
        }

        if (entity.CreatedBy != _user.Id && !await _identityService.IsInRoleAsync(_user.Id!, Roles.Administrator))
        {
            return Result.Failure([]);
        }

        _context.Sheets.Remove(entity);

        entity.AddDomainEvent(new SheetDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
