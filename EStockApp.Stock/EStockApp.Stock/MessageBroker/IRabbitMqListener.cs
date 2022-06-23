using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockApp.Stock.MessageBroker
{
    public interface IRabbitMqListener
    {
        void Receive();
        void Publish(string message);
    }
}
