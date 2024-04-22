
using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Domain.Events.Sheets;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;

public record DeleteSheetCommand(int Id) : IRequest<Response>;

public class DeleteSheetCommandHandler(
    IApplicationDbContext context,
    IUser user,
    IAuthService authorizationService) : IRequestHandler<DeleteSheetCommand, Response>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IAuthService _authorizationService = authorizationService;

    public async Task<Response> Handle(DeleteSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id], cancellationToken);

        var response = await _authorizationService.ValidateEntityAccess<Response>(entity, _user.Id!);

        if (response is not null)
        {
            return response;
        }

        _context.Sheets.Remove(entity!);

        entity!.AddDomainEvent(new SheetDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Response.Success(HttpStatusCode.NoContent);
    }
}
