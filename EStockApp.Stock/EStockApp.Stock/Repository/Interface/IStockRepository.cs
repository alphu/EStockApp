using EStockMarket.Stock.Dto;
using EStockMarket.Stock.Models;
using System;
using System.Threading.Tasks;

namespace EStockMarket.Stock.Repository
{
    public interface IStockRepository
    {
        public Task<CompanyDto> GetStockDetailsByDateRangeAsync(int companyCode, DateTime startDate, DateTime endDate);
        public Task<bool> AddStockDetails(StockModel stock, int companyCode);
        public Task<StockModel> GetStockPriceByCompanyCode(int companyCode);

    }
}
