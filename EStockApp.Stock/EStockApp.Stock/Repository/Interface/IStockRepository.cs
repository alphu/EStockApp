using EStockMarket.Stock.Dto;
using EStockMarket.Stock.Models;
using System;
using System.Threading.Tasks;

namespace EStockMarket.Stock.Repository
{
    public interface IStockRepository
    {
        public Task<CompanyDto> GetStockDetailsByDateRangeAsync(int code, DateTime startDate, DateTime endDate);
        public Task<bool> AddStockDetails(StockModel stock, int code);

    }
}
