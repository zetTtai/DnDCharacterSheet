
using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Events.Sheets;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;

public record DeleteSheetCommand(int Id) : IRequest<Result>;

public class DeleteSheetCommandHandler(
    IApplicationDbContext context,
    IUser user,
    IAuthService authorizationService) : IRequestHandler<DeleteSheetCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IAuthService _authorizationService = authorizationService;

    public async Task<Result> Handle(DeleteSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id], cancellationToken);

        var result = await _authorizationService.ValidateEntityAccess<Result>(entity, _user.Id!);

        if (result is not null)
        {
            return result;
        }

        _context.Sheets.Remove(entity!);

        entity!.AddDomainEvent(new SheetDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(HttpStatusCode.NoContent);
    }
}
