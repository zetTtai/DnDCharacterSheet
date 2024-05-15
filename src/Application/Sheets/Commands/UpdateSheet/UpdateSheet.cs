using System.Diagnostics;
using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Security;
using DnDCharacterSheet.Domain.ValueObjects;
using DnDCharacterSheet.Domain.Entities;


namespace DnDCharacterSheet.Application;

[Authorize]
public class UpdateSheetCommand : IRequest<Response>
{
    private int _id;
    public required string CharacterName { get; set; }
    public Money Money { get; set; } = new();

    public void Id(int id) => _id = id;
    public int Id() => _id;
}

public class UpdateSheetCommandHandler(
    IApplicationDbContext context,
    IUser user,
    IAuthService authorizationService) : IRequestHandler<UpdateSheetCommand, Response>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IAuthService _authorizationService = authorizationService;

    public async Task<Response> Handle(UpdateSheetCommand request, CancellationToken cancellationToken)
    {
        Debug.Assert(_user.Id is not null, "User ID should never be null here due to AuthorizationBehaviour middleware check.");

        var entity = await _context.Sheets.FindAsync([request.Id()], cancellationToken: cancellationToken);

        if (entity is null)
        {
            return Response.Failure(HttpStatusCode.NotFound, [$"Sheet with ID {request.Id()} not found."]);
        }

        var isOwner = await _authorizationService.IsOwner(entity, _user.Id);
        if (!isOwner)
        {
            return Response.Failure(HttpStatusCode.Forbidden, []);
        }

        entity.CharacterName = request.CharacterName;
        entity.Money = request.Money;


        await _context.SaveChangesAsync(cancellationToken);

        return Response.Success(HttpStatusCode.NoContent);
    }
}
