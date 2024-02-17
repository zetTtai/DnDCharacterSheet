using DnDCharacterSheet.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DnDCharacterSheet.Models;

namespace DnDCharacterSheet.Controllers
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

        [HttpPut("{id}/scores/str")]
        public ActionResult<SheetDTO> SetStrengthScore(int id, [FromBody] StrengthScoreRequest request)
        {
            // TODO: Get sheet by Id?
            Sheet sheetToModify = new(id);
            Sheet sheet = _sheetService.SetStrenghtScore(sheetToModify, request.Value);
            return Ok(sheet);
        }

    }

    public class StrengthScoreRequest
    {
        public int Value { get; set; }
    }
}
