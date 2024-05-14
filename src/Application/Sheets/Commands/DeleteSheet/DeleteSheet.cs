
using System.Diagnostics;
using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Security;
using DnDCharacterSheet.Domain.Entities;
using DnDCharacterSheet.Domain.Events.Sheets;

namespace DnDCharacterSheet.Application.Sheets.Commands.DeleteSheet;

[Authorize]
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
        Debug.Assert(_user.Id is not null, "User ID should never be null here due to middleware check.");

        var entity = await _context.Sheets.FindAsync([request.Id], cancellationToken);

        if (entity is null)
        {
            return Response.Failure(HttpStatusCode.NotFound, [$"Sheet with ID {request.Id} not found."]);
        }

        var isOwner = await _authorizationService.IsOwner(entity, _user.Id);
        if (!isOwner)
        {
            return Response.Failure(HttpStatusCode.Forbidden, []);
        }

        _context.Sheets.Remove(entity);

        entity.AddDomainEvent(new SheetDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Response.Success(HttpStatusCode.NoContent);
    }
}
