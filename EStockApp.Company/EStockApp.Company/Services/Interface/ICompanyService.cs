using EStockMarket.Company.Dto;
using EStockMarket.Company.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EStockMarket.Company.Services
{
    public interface ICompanyService
    {
        public Task<List<CompanyDto>> GetAllCompaniesAsync();
        public Task<CompanyDto> GetCompanyByCodeAsync(int code);
        public Task<CompanyModel> AddCompany(CompanyModel company);
        public Task<bool> DeleteCompany(int code);
        public Task<List<StockModel>> GetAllStockAsync();
        public Task<CompanyModel> UpdateCompany(CompanyModel companyDetails, int code);
    }
}
