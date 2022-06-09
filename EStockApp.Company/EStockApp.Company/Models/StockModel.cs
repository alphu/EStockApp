using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace EStockMarket.Company.Models
{
    public class StockModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CompanyCode { get; set; }

    }
}
