namespace DnDCharacterSheet.Application;

public class SheetUserListItemVm
{
    public int Id { get; set; }
    public string? CharacterName { get; set; }
    public bool IsModifiedByAdmin { get; set; } = false;
    public DateTimeOffset LastModified { get; set; }
}
