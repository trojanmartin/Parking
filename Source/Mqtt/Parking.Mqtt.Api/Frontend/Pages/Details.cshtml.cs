using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Database;
using Parking.Database.Entities;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public MqttServerConfiguration MqttServerConfiguration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MqttServerConfiguration = await _context.MqttServerConfigurations.FirstOrDefaultAsync(m => m.Id == id);

            if (MqttServerConfiguration == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
