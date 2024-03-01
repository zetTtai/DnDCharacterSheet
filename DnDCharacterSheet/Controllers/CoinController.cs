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
                CoinDTO coin = _service.AddCoin(new CoinDTO()
                {
                    Name = request.Name ?? throw new BadRequestException(),
                    Initials = request.Initials ?? throw new BadRequestException()
                });
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
            catch (BadRequestException ex)
            {
                return BadRequest(new ErrorDTO()
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
            }
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
                return BadRequest(new ErrorDTO()
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
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
                return BadRequest(new ErrorDTO()
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
            }
        }
    }
}
