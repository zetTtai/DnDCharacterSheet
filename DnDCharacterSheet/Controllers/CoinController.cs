using DnDCharacterSheet;
using DTOs;
using Exceptions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace Controllers
{
    [ApiController]
    [Route("coins")]
    public class CoinController(
        ILogger<CoinController> logger,
        ICoinService service
        ) : ControllerBase
    {
        private readonly ILogger<CoinController> _logger = logger ?? throw new ArgumentNullException();
        private readonly ICoinService _service = service ?? throw new ArgumentNullException();


        [HttpPost]
        public ActionResult<SheetDTO> Create([FromBody] CoinRequestDTO request)
        {
            try
            {
                return Ok();
            }
            catch (BadRequestException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<CoinDTO>> GetCoins()
        {
            var coins = _service.GetAllCoins();

            return Ok(coins);
        }

        [HttpPut("{id}")]
        public ActionResult<SheetDTO> Update(int id, [FromBody] CoinRequestDTO request)
        {
            try
            {
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<SheetDTO> Delete(int id, [FromBody] CoinRequestDTO request)
        {
            try
            {
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
