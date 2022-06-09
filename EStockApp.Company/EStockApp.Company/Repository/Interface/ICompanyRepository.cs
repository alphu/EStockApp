using EStockMarket.Company.Dto;
using EStockMarket.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockMarket.Company.Repository
{
    public interface ICompanyRepository
    {
        public Task<List<CompanyDto>> GetAllCompaniesAsync();
        public Task<CompanyDto> GetCompanyByCodeAsync(int code);
        public Task<CompanyModel> AddCompany(CompanyModel company);
        public Task<bool> DeleteCompany(int code);
    }
}
