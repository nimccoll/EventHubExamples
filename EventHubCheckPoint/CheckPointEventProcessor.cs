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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EventHubCheckPoint
{
    public class CheckPointEventProcessor : IEventProcessor
    {
        public void Dispose()
        {
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Trace.TraceInformation("CheckPointEventProcessor Shutting Down. Partition '{0}', Reason: '{1}'.", context.Lease.PartitionId, reason);
            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }

        public Task OpenAsync(PartitionContext context)
        {
            Trace.TraceInformation("CheckPointEventProcessor initialized.  Partition: '{0}', Offset: '{1}'", context.Lease.PartitionId, context.Lease.Offset);
            return Task.FromResult<object>(null);
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            Trace.TraceInformation("CheckPointEventProcessor processing {0} messages from partition {1}...", messages.ToList().Count, context.Lease.PartitionId);
            await context.CheckpointAsync();
        }
    }
}
