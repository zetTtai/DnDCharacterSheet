using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;

namespace DnDCharacterSheet.Application;

public class UpdateSheetCommand : IRequest<Response>
{
    private int _id;
    public required string CharacterName { get; set; }

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
        var entity = await _context.Sheets.FindAsync([request.Id()], cancellationToken: cancellationToken);

        var response = await _authorizationService.ValidateEntityAccess<Response>(entity, _user.Id!);

        if (response is not null)
        {
            return response;
        }

        entity!.CharacterName = request.CharacterName;

        await _context.SaveChangesAsync(cancellationToken);

        return Response.Success(HttpStatusCode.NoContent);
    }
}
