using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EStockMarket.Company.Models;
using EStockMarket.Company.Dto;
using EStockMarket.Company.Services;

namespace EStockMarket.Company.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/market/[controller]")]
    //[Route("api/market/[controller]")]
    public class CompanyController : ControllerBase
    {
        
        private readonly ILogger<CompanyController> _logger;
        private ICompanyService _companyService= null;
        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public string HealthCheck()
        {
            _logger.LogInformation($"CompanyController Index executed at {DateTime.UtcNow}");

            return "Company Service is up and running";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CompanyModel company)
        {
            try
            {
                _logger.LogInformation($"Register New Company: {company}");

                var response = await _companyService.AddCompany(company);                
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }
        [Route("UpdateCompany/{code}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyModel company, int code)
        {
            try
            {
                _logger.LogInformation($"Register New Company: {company}");

                var response = await _companyService.UpdateCompany(company,code);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            try
            {
                var response = await _companyService.GetAllCompaniesAsync();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("getallStock")]
        public async Task<IActionResult> GetAllStock()
        {
            try
            {
                var stock = await _companyService.GetAllStockAsync();
                if (stock == null)
                {
                    return NotFound();
                }

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("info/{companyCode}")]
        public async Task<IActionResult> GetCompanyByCodeAsync(int companyCode)
        {
            try
            {
                var response = await _companyService.GetCompanyByCodeAsync(companyCode);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                return new BadRequestObjectResult(ex.Message.ToString());
            }
        }

        [HttpDelete("delete/{companyCode}")]
        public async Task<IActionResult> DeleteCompany(int companyCode)
        {
            try
            {
                var response = await _companyService.DeleteCompany(companyCode);
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
