namespace DTOs;

public record SheetDTO(
    int Id,
    IEnumerable<AttributeDTO> Attributes,
    IEnumerable<CapabilityDTO> Skills,
    IEnumerable<CapabilityDTO> SavingThrows);
