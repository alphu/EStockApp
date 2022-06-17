using EStockMarket.Company.Dto;
using EStockMarket.Company.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Xunit;

namespace EStockMarket.Company.Services.Tests
{
  
    public class CompanyServiceTests
    {
         
        [Fact]
        public void GetCompanyByCodeAsyncTest()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            Mock<IConfigurationSection> section = new Mock<IConfigurationSection>();
            AppConfig appconfig = new AppConfig()
            {
                DbConnectionString = "mongodb://localhost:27017/",
                DataBaseName = "EStock",
                CompanyCollectionName = "Company",
                StockCollectionName = "Stock"
            };
            Mock<IOptions<AppConfig>> settings = new Mock<IOptions<AppConfig>>();
            settings.Setup(x => x.Value).Returns(appconfig);
            Mock<CompanyRepository> chk = new Mock<CompanyRepository>(settings.Object);
            CompanyService service = new CompanyService(chk.Object, config.Object);
            var result = service.GetCompanyByCodeAsync(1);
            Assert.IsNotNull(result);
        }

        [Fact]
        public void GetCompanyByCodeAsyncTest_Failure()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            Mock<IConfigurationSection> section = new Mock<IConfigurationSection>();
            AppConfig appconfig = new AppConfig()
            {
                DbConnectionString = "mongodb://localhost:27017/",
                DataBaseName = "EStock",
                CompanyCollectionName = "Company",
                StockCollectionName = "Stock"
            };
            Mock<IOptions<AppConfig>> settings = new Mock<IOptions<AppConfig>>();
            settings.Setup(x => x.Value).Returns(appconfig);
            Mock<CompanyRepository> chk = new Mock<CompanyRepository>(settings.Object);
            CompanyService service = new CompanyService(chk.Object, config.Object);
            chk.Setup(x => x.GetCompanyByCodeAsync(It.IsAny<int>())).ThrowsAsync(new Exception());
            //var result = service.GetCompanyByCodeAsync(0);
            Assert.ThrowsException<Exception>(() => service.GetCompanyByCodeAsync(0));
        }
    }
}