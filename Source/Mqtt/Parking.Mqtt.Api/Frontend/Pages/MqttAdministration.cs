using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;

namespace Parking.Mqtt.Api
{
    public class MqttAdministrationModel : PageModel
    {

        [BindProperty]
        public GetStatusResponse Status { get; set; }

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