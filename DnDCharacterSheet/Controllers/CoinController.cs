using Config;
using DTOs;
using Exceptions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<CoinDTO> CreateCoin([FromBody] CoinRequestDTO request)
        {
            try
            {
                // TODO: Verify user is admin
                CoinDTO coin = _service.AddCoin(request);
                return Ok(coin);
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

        [HttpGet]
        public ActionResult<IEnumerable<CoinDTO>> GetCoins()
        {
            var coins = _service.GetAllCoins();
            return Ok(coins);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CoinDTO>> GetCoin(long id)
        {
            try
            {
                var coin = _service.GetCoinById(id);
                return Ok(coin);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new ErrorDTO()
                {
                    StatusCode = 404,
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<SheetDTO> Update(int id, [FromBody] CoinRequestDTO request)
        {
            try
            {
                // TODO: Verify user is admin
                var coin = _service.UpdateCoin(id, request);
                return Ok(coin);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ErrorDTO()
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorDTO()
                {
                    StatusCode = 404,
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromBody] CoinRequestDTO request)
        {
            // TODO: Verify user is admin
            return _service.DeleteCoin(id)
                ? Ok(Constants.CoinService.CoinDeleted)
                : NotFound(Constants.CoinService.NoCoinFoundError);
        }
    }
}
