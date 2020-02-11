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
        private readonly IndexWebPresenter _indexPresenter;

        public IndexModel(IMQTTHandler handler, IndexWebPresenter configurationPresenter)
        {
            _handler = handler;
            _indexPresenter = configurationPresenter;
            Configuration = new List<MqttConfigurationViewModel>();
        }

        public IList<MqttConfigurationViewModel> Configuration { get; set; }        

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _handler.GetConfigurationAsync(new GetConfigurationRequest(null), _indexPresenter);

            if (result)
            {
                Configuration = _indexPresenter.Configurations;
                return Page();
            }

            else
                return RedirectToPage("Error");
        }

        public async Task<IActionResult> OnPostConnectAsync(int id)
        {          

            var res = await _handler.ConnectAsync(id, _indexPresenter);

            if (res)
            {
                Configuration = _indexPresenter.Configurations;                            
                return Page();
            }             

            else
                return RedirectToPage("Error");            
        }

        public async Task<IActionResult> OnPostDisconnectAsync(int id)
        {

            var disconnect = await _handler.DisconnectAsync(new DisconnectRequest(), _indexPresenter);
            var getAll = await _handler.GetConfigurationAsync(new GetConfigurationRequest(null), _indexPresenter);
            if (disconnect && getAll)
            {
                 
                Configuration = _indexPresenter.Configurations;               
                return Page();
            }

            else
                return RedirectToPage("Error");
        }
    }
}
