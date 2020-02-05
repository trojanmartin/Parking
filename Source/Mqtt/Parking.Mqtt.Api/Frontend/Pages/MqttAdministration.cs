using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class MqttAdministrationModel : PageModel
    {

        [BindProperty]
        public MqttStatus Status { get; set; }

        private readonly GetStatusWebPresenter _presenter;
        private readonly IGetStatusUseCase _getStatusUseCase;

        public MqttAdministrationModel(GetStatusWebPresenter presenter, IGetStatusUseCase getStatusUseCase)
        {
            _presenter = presenter;
            _getStatusUseCase = getStatusUseCase;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _getStatusUseCase.HandleAsync(new GetStatusRequest(), _presenter);

            Status = _presenter.Response;

            return Page();
        }
    }
}