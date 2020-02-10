using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Mqtt.Api.Frontend.Models;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class IndexModel : PageModel
    {
        private readonly IMQTTHandler _handler;
        private readonly ConfigurationWebPresenter _configurationPresenter;

        public IndexModel(IMQTTHandler handler, ConfigurationWebPresenter configurationPresenter)
        {
            _handler = handler;
            _configurationPresenter = configurationPresenter;
            Configuration  = new List<MqttConfigurationViewModel>();
        }

        public IList<MqttConfigurationViewModel> Configuration { get; set; } 

        public MqttConfigurationViewModel Connected { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _handler.GetConfigurationAsync(new GetConfigurationRequest(null), _configurationPresenter);

            if (result)
            {
                Configuration = _configurationPresenter.Configurations;
                return Page();
            }
                

            else
                return RedirectToPage("Error");
        }

        public async Task<IActionResult> OnPostConnectAsync(int id)
        {
            return Page();

            var connect = Configuration.First(x => x.Id == id);

            var res = await _handler.ConnectAsync(new ConnectRequest(new MQTTServerConfigurationDTO(connect.ClientId, connect.TcpServer, connect.Port,
                                      connect.Username, connect.Password, connect.UseTls, connect.CleanSession, connect.KeepAlive)), _configurationPresenter);

            if (res)
                Connected = connect;

            else
                return RedirectToPage("Error");

            return Page();
        }
    }
}
