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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;            
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
