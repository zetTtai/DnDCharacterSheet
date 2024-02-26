using DTOs;
using Exceptions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("sheet")]
    public class SheetController(
        ILogger<SheetController> logger,
        ISheetService sheetService,
        IConverter<Sheet, SheetDTO> sheetConverter,
        ISettingAttributeStrategyFactory settingAttributeStrategyFactory,
        IUtilsService utilsService
        ) : ControllerBase
    {

        private readonly ILogger<SheetController> _logger = logger ?? throw new ArgumentNullException();
        private readonly ISheetService _sheetService = sheetService ?? throw new ArgumentNullException();
        private readonly IConverter<Sheet, SheetDTO> _sheetConverter = sheetConverter ?? throw new ArgumentNullException();
        private readonly ISettingAttributeStrategyFactory _settingAttributeStrategyFactory = settingAttributeStrategyFactory ?? throw new ArgumentNullException();
        private readonly IUtilsService _utilsService = utilsService ?? throw new ArgumentNullException();

        [HttpPut("{id}/attributes/{attribute}")]
        public ActionResult<SheetDTO> SetStrengthAttribute(int id, string attribute, [FromBody] SetStrengthAttributeDTO request)
        {
            try
            {
                // TODO: Get sheet by Id?
                Sheet sheetToModify = new(id);
                _sheetService.SetAttributeSettingStrategy(_settingAttributeStrategyFactory.CreateStrategy(
                    request.Method,
                    _utilsService.StringToCharacterAttribute(attribute)));
                Sheet sheet = _sheetService.SetAttribute(sheetToModify, request.Value);
                return Ok(_sheetConverter.Convert(sheet));
            } catch(BadRequestException ex)
            {
                return BadRequest(new ErrorDTO()
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
            }
        }
    }
}
