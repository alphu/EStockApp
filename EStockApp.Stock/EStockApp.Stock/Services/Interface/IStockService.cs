using EStockMarket.Stock.Dto;
using EStockMarket.Stock.Models;
using System;
using System.Threading.Tasks;

namespace EStockMarket.Stock.Services
{
    public interface IStockService
    {
        public Task<CompanyDto> GetStockDetailsByDateRangeAsync(int code, DateTime startDate, DateTime endDate);
        public Task<bool> AddStockDetails(StockModel stock, int code);
    }
}
