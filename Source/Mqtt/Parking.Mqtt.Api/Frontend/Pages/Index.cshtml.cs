using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Api.Frontend.Pages.Models;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class IndexModel : PageModel
    {

        private readonly GetStatusWebPresenter _presenter;
        private readonly IGetStatusUseCase _getStatusUseCase;

        [BindProperty]
        public MqttStatusViewModel Status { get; set; }

        public IndexModel(GetStatusWebPresenter presenter, IGetStatusUseCase getStatusUseCase)
        {           
            _presenter = presenter;
            _getStatusUseCase = getStatusUseCase;
        }

        public MqttStatusViewModel MqttStatusViewModel { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await _getStatusUseCase.HandleAsync(new GetStatusRequest(), _presenter);

            Status = _presenter.Response;

            return Page();
        }
    }
}
