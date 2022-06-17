using EStockMarket.Company.CustomExceptions;
using EStockMarket.Company.Dto;
using EStockMarket.Company.Models;
using EStockMarket.Company.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockMarket.Company.Services
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _companyRepository;
        private IConfiguration _config;
        public CompanyService(ICompanyRepository companyRepository, IConfiguration config)
        {
            _companyRepository = companyRepository;
            _config = config;
        }
        public async Task<CompanyDto> GetCompanyByCodeAsync(int code)
        {
            try
            {
                if (code <= 0)
                    throw new Exception("Please Enter Valid Code");
                var company = await _companyRepository.GetCompanyByCodeAsync(code);
                if (company == null)
                    throw new CompanyNotFound("No Company With The Given Code Found.");
                return company;
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
                var companyList = await _companyRepository.GetAllCompaniesAsync();
                if (companyList == null)
                    throw new CompanyNotFound("No Companies Found");
                return companyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<StockModel>> GetAllStockAsync()
        {
            try
            {
                var stockList = await _companyRepository.GetAllStockAsync();
                
                   
                return stockList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CompanyModel> AddCompany(CompanyModel companyDetails)
        {
            try
            {
                var result = await ValidateCompanyDetails(companyDetails);
                if (result)
                {
                    var company = await _companyRepository.AddCompany(companyDetails);
                    return company;
                }
                else
                    throw new Exception("Unable To Add Company");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CompanyModel> UpdateCompany(CompanyModel companyDetails, int code)
        {
            try
            {
                var result = await ValidateCompanyDetails(companyDetails);
                if (result)
                {
                    var company = await _companyRepository.UpdateCompany(companyDetails,code);
                    return company;
                }
                else
                    throw new Exception("Unable To Add Company");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteCompany(int code)
        {
            try
            {
                var result = await _companyRepository.DeleteCompany(code);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<bool> ValidateCompanyDetails(CompanyModel company)
        {
            if (company.Code <= 0)
                throw new CompanyValidationException("Please Enter Valid Code");
            var companycodeList = (await _companyRepository.GetAllCompaniesAsync()).ToList().Select(x => x.Code);

            if (companycodeList.Contains(company.Code))
                throw new CompanyValidationException("Please Enter Valid Code");

            if (String.IsNullOrEmpty(company.CEO))
                throw new CompanyValidationException("Please Enter Valid CEO");

            if (String.IsNullOrEmpty(company.Name))
                throw new CompanyValidationException("Please Enter Valid Name");

            if (company.Turnover <= 1)
                throw new CompanyValidationException("Turnover should be more than 10Cr");

            if (String.IsNullOrEmpty(company.StockExchange))
                throw new CompanyValidationException("Please Enter Valid Stock Exchange");

            if (String.IsNullOrEmpty(company.Website))
                throw new CompanyValidationException("Please Enter Valid Website");
            return true;
        }
    }
}
