using DTOs;
using Enums;
using Exceptions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

[ApiController]
[Route("sheets")]
public class SheetController(
    ILogger<SheetController> logger,
    ISheetService sheetService,
    IConverter<Sheet, SheetDTO> sheetConverter,
    ISettingAbilitiesStrategyFactory settingAbilityStrategyFactory
    ) : ControllerBase
{

    private readonly ILogger<SheetController> _logger = logger ?? throw new ArgumentNullException();
    private readonly ISheetService _sheetService = sheetService ?? throw new ArgumentNullException();
    private readonly IConverter<Sheet, SheetDTO> _sheetConverter = sheetConverter ?? throw new ArgumentNullException();
    private readonly ISettingAbilitiesStrategyFactory _settingAbilityStrategyFactory = settingAbilityStrategyFactory ?? throw new ArgumentNullException();

    [HttpPut("{id}/abilities/{ability}")]
    public ActionResult<SheetDTO> SetAbility(int id, CharacterAbilities ability, [FromBody] SetAbilityRequestDTO request)
    {
        try
        {
            // TODO: Get sheet by Id
            Sheet sheetToModify = new(id);
            _sheetService.SetAbilitySettingStrategy(_settingAbilityStrategyFactory.CreateStrategy(request.Method));
            Sheet sheet = _sheetService.SetAbility(sheetToModify, request.Value, ability);
            return Ok(_sheetConverter.Convert(sheet));
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new ErrorDTO(400, ex.Message));
        }
    }
}
