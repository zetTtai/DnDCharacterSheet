namespace DTOs;

public record SheetDTO(
    int Id,
    IEnumerable<AbilityDTO> Abilities,
    IEnumerable<CapabilityDTO> Skills,
    IEnumerable<CapabilityDTO> SavingThrows);
