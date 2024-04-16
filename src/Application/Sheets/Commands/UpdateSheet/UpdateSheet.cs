using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;

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
    IIdentityService identityService) : IRequestHandler<UpdateSheetCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> Handle(UpdateSheetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sheets.FindAsync([request.Id()], cancellationToken: cancellationToken);

        if (entity is null)
        {
            return Result.Failure([]);
        }

        if (entity.CreatedBy != _user.Id && !await _identityService.IsInRoleAsync(_user.Id!, Roles.Administrator))
        {
            return Result.Failure([]);
        }

        entity.CharacterName = request.CharacterName;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
