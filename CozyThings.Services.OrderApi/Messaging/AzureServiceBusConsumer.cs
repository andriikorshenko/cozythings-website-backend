﻿using AutoMapper;
using Azure.Messaging.ServiceBus;
using CozyThings.Services.OrderApi.Data.Entities;
using CozyThings.Services.OrderApi.Models;
using CozyThings.Services.OrderApi.Repository.Imp;
using Newtonsoft.Json;
using System.Text;

namespace CozyThings.Services.OrderApi.Messaging
{
    public class AzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string checkoutMessageTopic;
        private readonly OrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AzureServiceBusConsumer(
            OrderRepository orderRepository, 
            IMapper mapper, 
            IConfiguration configuration)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.configuration = configuration;

            serviceBusConnectionString = this.configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = this.configuration.GetValue<string>("SubscriptionName");
            checkoutMessageTopic = this.configuration.GetValue<string>("CheckoutMessageTopic");
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            var orderHeader = mapper.Map<OrderHeader>(checkoutHeaderDto);

            foreach (var item in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    Id = item.Id,
                    ProductName = item.Product.Name,
                    ProductPrice = item.Product.Price,
                    Count = item.Count
                };
                orderHeader.CartTotalItems += item.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }
            await orderRepository.AddOrder(orderHeader);
        }
    }
}
