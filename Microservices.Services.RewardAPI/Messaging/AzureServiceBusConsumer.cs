﻿using Azure.Messaging.ServiceBus;
using Microservices.Services.RewardAPI.Message;
using Microservices.Services.RewardAPI.Services;
using Newtonsoft.Json;
using System.Text;

namespace Microservices.Services.RewardAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string orderCreatedTopic;
        private readonly string orderCreatedRewardSubscription;
        private readonly IConfiguration _configuration;
        private readonly RewardService _rewardService;

        private ServiceBusProcessor _rewardProcessor;
        public AzureServiceBusConsumer(IConfiguration configuration, RewardService rewardService)
        {
            _configuration = configuration;
            _rewardService = rewardService;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            orderCreatedRewardSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _rewardProcessor = client.CreateProcessor(orderCreatedTopic, orderCreatedRewardSubscription);
        }

        public async Task Start()
        {
            _rewardProcessor.ProcessMessageAsync += OnNewOrderRewardsRequestReceived;
            _rewardProcessor.ProcessErrorAsync += ErrorHandler;
            await _rewardProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _rewardProcessor.StopProcessingAsync();
            await _rewardProcessor.DisposeAsync();
        }

        private async Task OnNewOrderRewardsRequestReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            RewardMessage objMessage = JsonConvert.DeserializeObject<RewardMessage>(body);
            try
            {
                await _rewardService.UpdateReward(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}