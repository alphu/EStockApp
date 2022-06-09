using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EStockMarket.Company.Models
{
    public class CompanyModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string CEO { get; set; }    
        public int Turnover { get; set; }
        public string Website { get; set; }
        public string StockExchange { get; set; }
    }
}
