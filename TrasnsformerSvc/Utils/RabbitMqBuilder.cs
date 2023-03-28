using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boundaries.DocumentTransformation;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace TrasnsformerSvc.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RabbitMqBuilder<T>
    {
        private IBasicProperties _exchangeProperties;
        private Queue _queueSettings;
        private string _name;
        private Action<T> _actionToExecute;

        public IConnection RabbitConnection { get; set; }
        public IModel ExchangeModel { get; private set; }
        public EventingBasicConsumer EventingBasicConsumer { get; private set; }

        public RabbitMqBuilder(Queue queue, string connectionName)
        {
            _queueSettings = queue;
            _name = connectionName;
            BuildRabbitConection(_queueSettings, connectionName);
        }

        private void BuildRabbitConection(Queue queueSettings, string name)
        {
            var connectionFactory = new ConnectionFactory
            {
                UserName = queueSettings.Username,
                Password = queueSettings.Password,
                VirtualHost = queueSettings.VirtualHost,
                HostName = queueSettings.ServerUrl,
                Port = queueSettings.Port,
                RequestedHeartbeat =  TimeSpan.Parse((queueSettings.Heartbeat).ToString()),
                AutomaticRecoveryEnabled =  true
            };

            RabbitConnection = connectionFactory.CreateConnection(name);
            BindModels();
        }

        private void BindModels()
        {
            ExchangeModel = RabbitConnection.CreateModel();

            ExchangeModel.ExchangeDeclare(_queueSettings.Exchange, ExchangeType.Fanout, true);

            ExchangeModel.QueueDeclare(_queueSettings.QueueName, exclusive: false, autoDelete: false, durable:true);

            ExchangeModel.QueueBind(_queueSettings.QueueName, _queueSettings.Exchange, routingKey: "", arguments: new Dictionary<string, object>());
            ExchangeModel.BasicQos(
                prefetchSize: (ushort)_queueSettings.PrefetchSize, 
                prefetchCount: (ushort)_queueSettings.PrefetchCount, 
                global: false);

            _exchangeProperties = ExchangeModel.CreateBasicProperties();
        }

        public void  SetReceiver(Action<T> action)
        {
            EventingBasicConsumer = new EventingBasicConsumer(ExchangeModel);
            EventingBasicConsumer.Received += ReceivedMessageFormQueue;
            ExchangeModel.BasicConsume(_queueSettings.QueueName, false, EventingBasicConsumer);
            _actionToExecute = action;
        }

        private void ReceivedMessageFormQueue(object sender, BasicDeliverEventArgs e)
        {
            T message = default;
            try
            {
                message = GetMessageModel<T>(e.Body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                ExchangeModel.BasicAck(e.DeliveryTag, false);
                _actionToExecute(message);
            }
        }

        private T GetMessageModel<T>(ReadOnlyMemory<byte> body)
        {
            var message = Encoding.UTF8.GetString(body.ToArray());
            return  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
        }
    }
}
