using Azure.Messaging.ServiceBus;
using EStockMarket.Company.Models;
using Microsoft.Extensions.Options;
using Newtonsoft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStockApp.Company.MessageBroker
{
    public class ServiceBusSender:IServiceBusSender
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusSender(IOptions<AzureServiceBusConfiguration> serviceBusOptions)
        {
            _connectionString = serviceBusOptions.Value.ConnectionString;
            _queueName = serviceBusOptions.Value.QueueName;
        }

        public async void SendCompany(CompanyModel company)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                var sender = client.CreateSender(_queueName);

                var json = JsonConvert.SerializeObject(company);
                var message = new ServiceBusMessage(json);

                await sender.SendMessageAsync(message);
            }
        }
    }
}
