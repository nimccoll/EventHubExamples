//===============================================================================
// Microsoft FastTrack for Azure
// Event Hub Checkpoint Sample
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;

namespace EventHubCheckPoint
{
    class Program
    {
        static readonly string _eventHubName = ConfigurationManager.AppSettings["EventHubName"];
        static readonly string _consumerGroupName = ConfigurationManager.AppSettings["ConsumerGroupName"];
        static readonly string _eventHubConnectionString = ConfigurationManager.AppSettings["EventHubConnectionString"];
        static readonly string _storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

        static void Main(string[] args)
        {
            EventProcessorOptions options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            EventProcessorHost eventProcessorHost = new EventProcessorHost(Guid.NewGuid().ToString(), _eventHubName, _consumerGroupName, _eventHubConnectionString, _storageConnectionString);
            eventProcessorHost.RegisterEventProcessorAsync<CheckPointEventProcessor>(options).Wait();
            Console.WriteLine("*** Receiving Events. Press any key to stop.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}