using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Infrastructure.Data;
using Parking.Mqtt.Infrastructure.Data.Entities;

namespace Parking.Mqtt.Api
{
    public class DetailsTopicModel : PageModel
    {
        private readonly Parking.Mqtt.Infrastructure.Data.ApplicationDbContext _context;

        public DetailsTopicModel(Parking.Mqtt.Infrastructure.Data.ApplicationDbContext context)
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
