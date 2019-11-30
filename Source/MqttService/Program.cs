using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using MqttService.Models;

namespace MqttService
{
    class Program 
    {
        static async Task Main(string[] args)
        {

            var message = new MqttMessage()
            {
                Payload = "neviem dopoci",
                Topic = "martin/neviem",
                QoS = MqttMessageQoS.ExactlyOnce
            };

            var opt = new MqttOptions()
            {
                ClientId = "test",
                TcpServer = "broker.hivemq.com",
                Port = 1883,
                UseTls = false
                
            };

            var service = new MqttService(new MqttFactory().CreateMqttClient());

            await service.ReceivedMessage(async (data) =>
            {
                Console.WriteLine(data.ApplicationMessage.Topic);
                Console.WriteLine(Encoding.UTF8.GetString(data.ApplicationMessage.Payload));
            });

            await service.Connect(opt);

            await service.Subscribe(message.Topic);
            int a = 50;
            while(a > 0)
            {
                await service.PublishMessage(message);
                a--;
            }

            Console.ReadKey();


        }
    }
}
