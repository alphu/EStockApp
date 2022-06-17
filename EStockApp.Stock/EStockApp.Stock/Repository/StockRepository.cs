using EStockMarket.Stock.Dto;
using EStockMarket.Stock.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EStockMarket.Stock.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly IMongoCollection<CompanyModel> _company;
        private readonly IMongoCollection<StockModel> _stock;
        private readonly AppConfig _settings;
        public StockRepository(IOptions<AppConfig> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.DbConnectionString);
            var database = client.GetDatabase(_settings.DataBaseName);
            _company = database.GetCollection<CompanyModel>(_settings.CompanyCollectionName);
            _stock = database.GetCollection<StockModel>(_settings.StockCollectionName);
        }

        public async Task<bool> AddStockDetails(StockModel stock, int companyCode)
        {
            try
            {
                var company = await _company.Find(c => c.Code == companyCode).FirstOrDefaultAsync();
                if (company != null)
                {
                    await _stock.InsertOneAsync(stock);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyDto> GetStockDetailsByDateRangeAsync(int code, DateTime startDate, DateTime endDate)
        {
            try
            {
                var company = await _company.Find(c => c.Code == code).FirstOrDefaultAsync();
                var stockList = await _stock.Find(c => c.CompanyCode == code).ToListAsync();
                if (company != null)
                {
                    var result = new CompanyDto()
                    {
                        Code = company.Code,
                        Name = company.Name,
                        CEO = company.CEO,
                        Turnover = company.Turnover,
                        Website = company.StockExchange,
                        StockExchange = company.StockExchange,
                        Stock = stockList
                    };
                    return result;
                }
                return null;
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
                var stockList = await _stock.Find(c => c.CompanyCode == companyCode).ToListAsync();
                var latestStock = stockList.OrderByDescending(x => x.CreatedOnUtc).FirstOrDefault();
                if (latestStock != null)
                {
                    var result = new StockModel()
                    {
                        CompanyCode = latestStock.CompanyCode,
                        Price = latestStock.Price,

                    };
                    return result;
                }
                return null;
            }

            catch (Exception ex)
            {
                throw ex;
            }

}


    }
}
