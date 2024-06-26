﻿namespace DnDCharacterSheet.Application.Sheets.Queries.GetSheets;
public class SheetAdminListItemVm
{
    public int Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? CreatedByName { get; set; }
    public string? LastModifiedByName { get; set; }
}
