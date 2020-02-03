using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Core.Interfaces.UseCases;

namespace Parking.Mqtt.Api.Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        private readonly GetStatusWebPresenter _presenter;
        private readonly IGetStatusUseCase _getStatusUseCase;

        public IndexModel(ILogger<IndexModel> logger, GetStatusWebPresenter presenter, IGetStatusUseCase getStatusUseCase)
        {
            _logger = logger;
            _presenter = presenter;
            _getStatusUseCase = getStatusUseCase;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
