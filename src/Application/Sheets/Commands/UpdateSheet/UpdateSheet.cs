using System.Net;
using DnDCharacterSheet.Application.Common.Interfaces;

namespace DnDCharacterSheet.Application;

public class UpdateSheetCommand : IRequest<Result>
{
    private int _id;
    public required string CharacterName { get; set; }

    public void Id(int id) => _id = id;
    public int Id() => _id;
}

public class UpdateSheetCommandHandler(
    IApplicationDbContext context,
    IUser user,
    IAuthService authorizationService) : IRequestHandler<UpdateSheetCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IAuthService _authorizationService = authorizationService;

    public async Task<Result> Handle(UpdateSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id()], cancellationToken: cancellationToken);

        var result = await _authorizationService.ValidateEntityAccess<Result>(entity, _user.Id!);

        if (result is not null)
        {
            return result;
        }

        entity!.CharacterName = request.CharacterName;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(HttpStatusCode.NoContent);
    }
}
