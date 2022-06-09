using EStockMarket.Stock.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace EStockMarket.Stock.Dto
{
    public class CompanyDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string CEO { get; set; }
        public int Turnover { get; set; }
        public string Website { get; set; }
        public string StockExchange { get; set; }
        public StockModel LatestStock { get; set; }
        public List<StockModel> Stock { get; set; }
    }
}
