//using Parking.Mqtt.Api.Frontend.Models;
//using Parking.Mqtt.Core.Interfaces;
//using Parking.Mqtt.Core.Models.UseCaseResponses;
//using System;

//namespace Parking.Mqtt.Api.Frontend.Presenters
//{
//    public class GetStatusWebPresenter : IOutputPort<GetStatusResponse>
//    {
//        public MqttStatusViewModel Response { get; set; }

//        public void CreateResponse(GetStatusResponse response)
//        {

//            if (response.Success)
//            {
//                Response = new MqttStatusViewModel
//                {
//                    ClientId = response?.Status.ClientId,
//                    Connected = (bool)response?.Status.Connected,
//                    TcpServer = response?.Status.TcpServer,
//                    ID = Convert.ToInt32(response?.Status.TcpServer),
//                    Port = response?.Status.Port
//                };
//            }
//        }
//    }
//}
