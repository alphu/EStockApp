using EStockMarket.Company.Dto;
using EStockMarket.Company.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockMarket.Company.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<CompanyModel> _company;
        private readonly IMongoCollection<StockModel> _stock;
        private readonly AppConfig _settings;
        public CompanyRepository(IOptions<AppConfig> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.DbConnectionString);
            var database = client.GetDatabase(_settings.DataBaseName);
            _company = database.GetCollection<CompanyModel>(_settings.CompanyCollectionName);
            _stock = database.GetCollection<StockModel>(_settings.StockCollectionName);
        }
        public async Task<CompanyDto> GetCompanyByCodeAsync(int code)
        {
            try
            {
                var company = await _company.Find(c => c.Code == code).FirstOrDefaultAsync();
                var stockList = await _stock.Find(c => c.CompanyCode == code).ToListAsync();
                var latestStock = stockList.OrderByDescending(x => x.CreatedOnUtc).FirstOrDefault();
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
                        LatestStock = latestStock
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
        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            try
            {
                var companyList = await _company.Find(c => true).ToListAsync();
                var stockList = await _stock.Find(c => true).ToListAsync();
                var result = new List<CompanyDto>();
                foreach (var c in companyList)
                {
                    var latestStock = stockList.Where(y => y.CompanyCode == c.Code).OrderByDescending(x => x.CreatedOnUtc).FirstOrDefault();
                    var companyDet = new CompanyDto()
                    {
                        Code = c.Code,
                        Name = c.Name,
                        CEO = c.CEO,
                        Turnover = c.Turnover,
                        Website = c.StockExchange,
                        StockExchange = c.StockExchange,
                        LatestStock = latestStock
                    };
                    result.Add(companyDet);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyModel> AddCompany(CompanyModel company)
        {
            try
            {
                await _company.InsertOneAsync(company);
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CompanyModel> UpdateCompany(CompanyModel CompanyDto,int code)
        {
            if (CompanyDto != null)
            {
                var company = _company.Find(c => c.Code == CompanyDto.Code).FirstOrDefaultAsync();
                if (company != null)
                {
                    _company.ReplaceOne(s => s.Code == code, CompanyDto);
                    
                }
            } 
            else
            {

                return null;
            }
            return CompanyDto;
        }
        public async Task<List<StockModel>> GetAllStockAsync()
        {
            var stockList = await _stock.Find(c => true).ToListAsync();
 
            return stockList;
        }

        public async Task<bool> DeleteCompany(int code)
        {
            try
            {
                var company = await _company.Find(c => c.Code == code).FirstOrDefaultAsync();
                if (company != null)
                {
                    await _stock.DeleteManyAsync(x => x.CompanyCode == code);
                    await _company.DeleteOneAsync(x => x.Code == code);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
