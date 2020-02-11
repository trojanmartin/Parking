using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Api.Frontend.Models;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class EditModel : PageModel
    {
        private readonly IMQTTHandler _handler;
        private readonly EditWebPresenter _presenter;

        public EditModel(IMQTTHandler handler, EditWebPresenter presenter)
        {
            _handler = handler;
            _presenter = presenter;
        }

        [BindProperty]
        public MqttConfigurationViewModel MqttServerConfiguration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _handler.GetConfigurationAsync(new GetConfigurationRequest(id),_presenter);

            if (res)
                MqttServerConfiguration = _presenter.ConfigurationViewModel;

            if (MqttServerConfiguration == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }         

            
            if(await _handler.UpdateConfigurationAsync(new SaveConfigurationRequest(VmToDto(MqttServerConfiguration)), _presenter))
                return RedirectToPage("./Index");

            else
                return RedirectToPage("Error");
        }       

        private MQTTServerConfigurationDTO VmToDto(MqttConfigurationViewModel model)
        {
            var topics = new List<MQTTTopicConfigurationDTO>();

            if (model.TopicSubscribing != null)
            {
               

                foreach (var topic in model.TopicSubscribing)
                {
                    var top = new MQTTTopicConfigurationDTO(topic.TopicName, topic.QoS);
                    topics.Add(top);
                }
            }
        

            return new MQTTServerConfigurationDTO(model.ClientId, model.TcpServer, model.Port, model.Username, model.Password, model.UseTls, model.CleanSession, model.KeepAlive, model.Id, topics, model.Name);
        }
    }
}
