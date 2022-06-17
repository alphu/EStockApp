using EStockMarket.Stock.CustomExceptions;
using EStockMarket.Stock.Dto;
using EStockMarket.Stock.Models;
using EStockMarket.Stock.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EStockMarket.Stock.Services
{
    public class StockService : IStockService
    {
        private IStockRepository _stockRepository;
        private IConfiguration _config;
        public StockService(IStockRepository stockRepository, IConfiguration config)
        {
            _stockRepository = stockRepository;
            _config = config;
        }
        public async Task<CompanyDto> GetStockDetailsByDateRangeAsync(int code, DateTime startDate, DateTime endDate)
        {
            try
            {
                if (code <= 0)
                    throw new Exception("Please Enter Valid Code");
                if (startDate > endDate)
                    throw new Exception("Please Enter Valid Date Range");
                var company = await _stockRepository.GetStockDetailsByDateRangeAsync(code, startDate, endDate);
                if (company == null)
                    throw new StockNotFound("No Company With The Given Code Found.");
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> AddStockDetails(StockModel stock, int code)
        {
            try
            {
                var result = ValidateStockDetails(stock);
                if (result)
                {
                    stock.CreatedOnUtc = DateTime.Now;
                    stock.CompanyCode = code;
                    var status = await _stockRepository.AddStockDetails(stock, code);
                    if (!status)
                        throw new StockNotFound("Company Not Found.");
                    return status;
                }
                else
                    throw new Exception("Unable To Add Company");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<StockModel> GetStockPriceByCompanyCode(int companyCode)
        {
            try
            {
                if (companyCode ==0)
                    throw new Exception("Please Enter Valid Code");
                var stock = await _stockRepository.GetStockPriceByCompanyCode(companyCode);
                if (stock == null)
                    throw new StockNotFound("No Stock With The Given Code Found.");
                return stock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateStockDetails(StockModel stock)
        {
            if (stock.Price <= 0.00)
                throw new StockPriceValidationException("Invalid Price");
            return true;
        }
    }
}
