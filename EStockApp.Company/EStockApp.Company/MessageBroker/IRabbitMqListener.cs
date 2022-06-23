using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockApp.Company.MessageBroker
{
    public interface IRabbitMqListener
    {
        void Receive();
        void Publish(string message);
    }
}
