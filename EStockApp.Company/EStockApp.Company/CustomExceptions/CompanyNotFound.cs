using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockMarket.Company.CustomExceptions
{
    public class CompanyNotFound : Exception
    {
        public CompanyNotFound(string message)
            : base(message)
        {
        }
        
    }   
    public class CompanyValidationException : Exception
    {
        public CompanyValidationException(string message)
            : base(message)
        {
        }
    }
}
