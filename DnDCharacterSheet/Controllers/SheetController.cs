using Converters;
using DTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Strategies;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SheetController(
        ILogger<SheetController> logger,
        ISheetService sheetService,
        IConverter<Sheet, SheetDTO> sheetConverter,
        IUtilsService utilsService
        ) : ControllerBase
    {

        private readonly ILogger<SheetController> _logger = logger ?? throw new ArgumentNullException();
        private readonly ISheetService _sheetService = sheetService ?? throw new ArgumentNullException();
        private readonly IConverter<Sheet, SheetDTO> _sheetConverter = sheetConverter ?? throw new ArgumentNullException();
        private readonly IUtilsService _utilsService = utilsService ?? throw new ArgumentNullException();

        [HttpPut("{id}/attributes/str")]
        public ActionResult<SheetDTO> SetStrengthAttribute(int id, [FromBody] SetStrengthAttributeDTO request)
        {
            try
            {
                // TODO: Get sheet by Id?
                Sheet sheetToModify = new(id);
                _sheetService.SetStrategy(new RollingDiceStrategy(_utilsService));
                Sheet sheet = _sheetService.SetStrengthAttribute(sheetToModify, request.Value);
                return Ok(_sheetConverter.Convert(sheet));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
