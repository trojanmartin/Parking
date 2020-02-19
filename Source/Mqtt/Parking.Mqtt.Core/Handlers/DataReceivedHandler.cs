using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Serialization;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Handlers
{
    public class DataReceivedHandler : IDataReceivedHandler
    {
        public async Task ProccesMQTTMessage(MQTTMessageDTO message)
        {

            var data = Serializer.DeserializeToObject<SensorData>(message.Payload);


         





        }
    }
}
