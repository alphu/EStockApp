using EStockMarket.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockApp.Company.MessageBroker
{
  public interface IServiceBusSender
    {
        void SendCompany(CompanyModel company);
    }
}
