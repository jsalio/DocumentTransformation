using Boundaries.DocumentTransformation;
using RabbitMQ.Client;
using System;
using TrasnsformerSvc.Contract;
using TrasnsformerSvc.Utils;

namespace TrasnsformerSvc.Runners
{
    public sealed class QueueRunner : IServiceRunner
    {
        private readonly bool _secondaryConfig;
        private readonly ServiceSettings _serviceSettings;
        private readonly IServiceRunner _serviceRunnerImplementation;
        private IConnection _Connection;

        public event ServiceReachedEventHandler OnServiceRunnerEventHandler;

        public QueueRunner(ServiceSettings apiSettings, Settings settings, bool isSecondary = false)
        {
            _secondaryConfig = isSecondary;
            _serviceSettings = apiSettings;
            if (settings.EnableSecondQueue && isSecondary == false)
            {
                _serviceRunnerImplementation = new QueueRunner(apiSettings, settings, true);
                _serviceRunnerImplementation.Build();
                _serviceRunnerImplementation.OnServiceRunnerEventHandler += _serviceRunnerImplementation_OnServiceRunnerEventHandler;
            }
        }

        void IServiceRunner.StopRunner()
        {
            _Connection?.Dispose();
        }

        void IServiceRunner.Build()
        {
            var queueSettings = _serviceSettings.GetQueueConfiguration(_secondaryConfig);
            if (_secondaryConfig)
            {
                queueSettings.QueueName = $"Document_{queueSettings.QueueName}";
            }
            RabbitMqBuilder<object> builder = new RabbitMqBuilder<object>(queueSettings, "PDFTransformation");
            _Connection = builder.RabbitConnection;
            builder.SetReceiver(ReceivedMessage);
        }


        private void _serviceRunnerImplementation_OnServiceRunnerEventHandler(object sender, ServiceReachedEventArgs e)
        {
            OnServiceRunnerEventHandler?.Invoke(this, e);
        }

        private void ReceivedMessage(object obj)
        {
            OnServiceRunnerEventHandler?.Invoke(this, new ServiceReachedEventArgs
            {
                DateTimeOffset = DateTimeOffset.Now,
                JsonData = $"Rabbit execute at {DateTimeOffset.Now} and send {obj.ToString()}",
                Type = _secondaryConfig ? QueueReceiverType.Documents : QueueReceiverType.Batches
            });
        }
    }
}
