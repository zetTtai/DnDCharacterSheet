
using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Application.Common.Models;
using DnDCharacterSheet.Application.Common.Security;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application.Sheets.Queries.GetSheets;

[Authorize(Roles = Roles.Administrator)]
public record GetSheetsWithPaginationQuery : IRequest<PaginatedList<SheetAdminDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSheetsQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identity) : IRequestHandler<GetSheetsWithPaginationQuery, PaginatedList<SheetAdminDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IIdentityService _identity = identity;

    public async Task<PaginatedList<SheetAdminDto>> Handle(GetSheetsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var sheetsQuery = _context.Sheets
            .AsNoTracking()
            .OrderBy(s => s.Id);
        var totalCount = await sheetsQuery.CountAsync(cancellationToken);
        var sheets = await sheetsQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var userIds = sheets.Select(s => s.CreatedBy).Union(sheets.Select(s => s.LastModifiedBy)).Distinct().ToList();
        Dictionary<string, string?> userNames = await _identity.GetUserNamesAsync(userIds!);

        List<SheetAdminDto> sheetAdminDtos = CombineSheetsWithUserNames(sheets, userNames!);

        return new PaginatedList<SheetAdminDto>(sheetAdminDtos, totalCount, request.PageNumber, request.PageSize);
    }

    private List<SheetAdminDto> CombineSheetsWithUserNames(List<Sheet> sheets, Dictionary<string, string> userNames)
    {
        var sheetAdminDtos = sheets.Select(_mapper.Map<SheetAdminDto>).ToList();
        sheetAdminDtos.ForEach(dto =>
        {
            if (dto.CreatedBy is not null && userNames.TryGetValue(dto.CreatedBy, out var createdByName))
            {
                dto.CreatedByName = createdByName;
            }

            if (dto.LastModifiedBy is not null && userNames.TryGetValue(dto.LastModifiedBy, out var lastModifiedByName))
            {
                dto.LastModifiedByName = lastModifiedByName;
            }
        });

        return sheetAdminDtos;
    }
}
