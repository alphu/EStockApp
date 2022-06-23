using EStockApp.Stock.MessageBroker;
using EStockMarket.Stock.Models;
using EStockMarket.Stock.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EStockMarket.Stock.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/market/[controller]")]
    public class StockController : ControllerBase
    {

        private readonly ILogger<StockController> _logger;
        private IStockService _stockService = null;
        private IRabbitMqListener _rabbitMqListener;
        public StockController(ILogger<StockController> logger, IStockService stockService, IRabbitMqListener rabbitMqListener)
        {
            _logger = logger;
            _stockService = stockService;
            _rabbitMqListener = rabbitMqListener;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public string HealthCheck()
        {
            _logger.LogInformation($"StockController Index executed at {DateTime.UtcNow}");

            return "Stock Service is up and running";
        }

        [HttpPost("add/{companyCode}")]
        public async Task<IActionResult> AddStockDetails([FromBody] StockModel stockDetails, int companyCode)
        {
            try
            {
                _logger.LogInformation($"Add Stock Details : {stockDetails}");
                var response = await _stockService.AddStockDetails(stockDetails, companyCode);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("get/{companyCode}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetStockDetailsByDateRangeAsync(int companyCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation("Get Stock Deatils: ", companyCode);
                var response = await _stockService.GetStockDetailsByDateRangeAsync(companyCode, startDate, endDate);
                _logger.LogInformation($"Stock Details {companyCode} : ", response);
                _rabbitMqListener.Receive();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("getStockPrice/{companyCode}")]
        public async Task<IActionResult> GetStockPriceByCompanyCode(int companyCode)
        {
            try
            {
                var response = await _stockService.GetStockPriceByCompanyCode(companyCode);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }
    }
}
