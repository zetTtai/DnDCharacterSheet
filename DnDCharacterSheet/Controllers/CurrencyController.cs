using Config;
using DTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrencyController(
        ILogger<CurrencyController> logger,
        ICurrencyService service
        ) : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger = logger ?? throw new ArgumentNullException();
        private readonly ICurrencyService _service = service ?? throw new ArgumentNullException();

        [HttpPost]
        public ActionResult<CurrencyDTO> CreateCurrency([FromBody] CurrencyRequestDTO request)
        {
            // TODO: Verify user is admin
            CurrencyDTO coin = _service.AddCurrency(request);
            return Ok(coin);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CurrencyDTO>> GetCurrencies()
        {
            var coins = _service.GetAllCurrencies();
            return Ok(coins);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CurrencyDTO>> GetCurrency(long id)
        {
            try
            {
                var coin = _service.GetCurrencyById(id);
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
        public ActionResult<SheetDTO> Update(int id, [FromBody] CurrencyRequestDTO request)
        {
            try
            {
                // TODO: Verify user is admin
                var coin = _service.UpdateCurrency(id, request);
                return Ok(coin);
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
        public ActionResult Delete(int id, [FromBody] DeleteCurrencyRequestDTO request)
        {
            // TODO: Verify user is admin
            return _service.DeleteCurrency(id)
                ? Ok(Constants.CurrencyService.CurrencyDeleted)
                : NotFound(Constants.CurrencyService.NoCurrencyFoundError);
        }
    }
}
