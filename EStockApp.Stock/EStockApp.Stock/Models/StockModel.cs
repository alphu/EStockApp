using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace EStockMarket.Stock.Models
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
