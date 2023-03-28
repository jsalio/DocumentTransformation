using System;

namespace TrasnsformerSvc.Contract
{
    public class ServiceReachedEventArgs : EventArgs
    {
        public string JsonData { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public QueueReceiverType Type { get; set; }
    }
}
