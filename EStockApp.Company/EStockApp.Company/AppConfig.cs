using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockMarket.Company
{
    public class AppConfig
    {
        public string DataBaseName {get;set;}
        public string CompanyCollectionName { get; set; }
        public string StockCollectionName { get; set; }
        public string DbConnectionString { get; set; }
    }
}
