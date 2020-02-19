using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Database;
using Parking.Database.Entities;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class DetailsTopicModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsTopicModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public MqttTopicConfiguration MqttTopicConfiguration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MqttTopicConfiguration = await _context.MqttTopicConfigurations.FirstOrDefaultAsync(m => m.Id == id);

            if (MqttTopicConfiguration == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
