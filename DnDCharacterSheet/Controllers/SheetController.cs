using DTOs;
using Exceptions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

[ApiController]
[Route("sheet")]
public class SheetController(
    ILogger<SheetController> logger,
    ISheetService sheetService,
    IConverter<Sheet, SheetDTO> sheetConverter,
    ISettingAbilitiesStrategyFactory settingAbilityStrategyFactory,
    IUtilsService utilsService
    ) : ControllerBase
{

    private readonly ILogger<SheetController> _logger = logger ?? throw new ArgumentNullException();
    private readonly ISheetService _sheetService = sheetService ?? throw new ArgumentNullException();
    private readonly IConverter<Sheet, SheetDTO> _sheetConverter = sheetConverter ?? throw new ArgumentNullException();
    private readonly ISettingAbilitiesStrategyFactory _settingAbilityStrategyFactory = settingAbilityStrategyFactory ?? throw new ArgumentNullException();
    private readonly IUtilsService _utilsService = utilsService ?? throw new ArgumentNullException();

    [HttpPut("{id}/attributes/{attribute}")]
    public ActionResult<SheetDTO> SetStrengthAttribute(int id, string ability, [FromBody] SetStrengthAttributeDTO request)
    {
        try
        {
            // TODO: Get sheet by Id?
            Sheet sheetToModify = new(id);
            _sheetService.SetAbilitySettingStrategy(_settingAbilityStrategyFactory.CreateStrategy(
                request.Method,
                _utilsService.StringToCharacterAbility(ability)));
            Sheet sheet = _sheetService.SetAbility(sheetToModify, request.Value);
            return Ok(_sheetConverter.Convert(sheet));
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new ErrorDTO()
            {
                StatusCode = 400,
                Message = ex.Message,
            });
        }
    }
}
