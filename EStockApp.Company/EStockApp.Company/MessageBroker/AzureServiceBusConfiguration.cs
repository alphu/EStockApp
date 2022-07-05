using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockApp.Company.MessageBroker
{
    public class AzureServiceBusConfiguration
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
