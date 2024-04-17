
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Security;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application.Sheets.Queries.GetSheets;

[Authorize(Roles = Roles.Administrator)]
public record GetSheetsWithPaginationQuery : IRequest<Result<PaginatedList<SheetAdminListItemVm>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSheetsQueryHandler(IApplicationDbContext context, IIdentityService identity) : IRequestHandler<GetSheetsWithPaginationQuery, Result<PaginatedList<SheetAdminListItemVm>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IIdentityService _identity = identity;

    public async Task<Result<PaginatedList<SheetAdminListItemVm>>> Handle(GetSheetsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Sheets
            .AsNoTracking()
            .OrderBy(s => s.Id);
        var totalCount = await query.CountAsync(cancellationToken);
        var sheets = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var userIds = sheets.Select(s => s.CreatedBy).Union(sheets.Select(s => s.LastModifiedBy)).Distinct().ToList();
        Dictionary<string, string?> userNames = await _identity.GetUserNamesAsync(userIds!);

        List<SheetAdminListItemVm> sheetAdminDtos = CombineSheetsWithUserNames(sheets, userNames!);

        var paginatedList = new PaginatedList<SheetAdminListItemVm>(sheetAdminDtos, totalCount, request.PageNumber, request.PageSize);
        return Result<PaginatedList<SheetAdminListItemVm>>.Success(paginatedList);
    }

    private List<SheetAdminListItemVm> CombineSheetsWithUserNames(List<Sheet> sheets, Dictionary<string, string> userNames)
    {
        var sheetAdminVms = sheets.Select(s => new SheetAdminListItemVm()
        {
            Id = s.Id,
            Created = s.Created,
            CreatedBy = s.CreatedBy,
            LastModified = s.LastModified,
            LastModifiedBy = s.LastModifiedBy,
            CreatedByName = s.CreatedBy is not null && userNames.TryGetValue(s.CreatedBy, out var createdByName)
                ? createdByName
                : "Unknown",
            LastModifiedByName = s.CreatedBy is not null && userNames.TryGetValue(s.CreatedBy, out var lastModifiedByName)
                ? lastModifiedByName
                : "Unknown"
        }).ToList();

        return sheetAdminVms;
    }
}
