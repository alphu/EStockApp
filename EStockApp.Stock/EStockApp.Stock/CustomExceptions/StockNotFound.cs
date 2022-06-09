using System;

namespace EStockMarket.Stock.CustomExceptions
{
    public class StockNotFound : Exception
    {
        public StockNotFound(string message)
            : base(message)
        {
        }

    }
    public class StockPriceValidationException : Exception
    {
        public StockPriceValidationException(string message)
            : base(message)
        {
        }
    }
}
