using DTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SheetController : ControllerBase
    {

        private readonly ILogger<SheetController> _logger;
        private readonly ISheetService _sheetService;

        public SheetController(ILogger<SheetController> logger, ISheetService sheetService)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _sheetService = sheetService ?? throw new ArgumentNullException();
        }

        [HttpPut("{id}/attributes/str")]
        public ActionResult<SheetDTO> SetStrengthAttribute(int id, [FromBody] SetStrengthAttributeDTO request)
        {
            // TODO: Get sheet by Id?
            Sheet sheetToModify = new(id);
            Sheet sheet;
            try
            {
                sheet = _sheetService.SetStrengthAttribute(sheetToModify, request.Value, request.Method);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //SheetDTO sheetDTO = new()
            //{
            //    StrengthScore = sheet.StrengthScore ?? "",
            //    Skills = _sheetService.ConvertToDTO(sheet.Skills, true),
            //    SavingThrows = _sheetService.ConvertToDTO(sheet.SavingThrows, false),
            //};
            return Ok();
        }

    }
}
