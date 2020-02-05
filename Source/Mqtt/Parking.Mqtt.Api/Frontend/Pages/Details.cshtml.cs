using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Api.Data;
using Parking.Mqtt.Api.Frontend.Pages.Models;

namespace Parking.Mqtt.Api
{
    public class DetailsModel : PageModel
    {
        private readonly Parking.Mqtt.Api.Data.ParkingMqttApiContext _context;

        public DetailsModel(Parking.Mqtt.Api.Data.ParkingMqttApiContext context)
        {
            _context = context;
        }

        public MqttStatusViewModel MqttStatusViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MqttStatusViewModel = await _context.MqttStatusViewModel.FirstOrDefaultAsync(m => m.ID == id);

            if (MqttStatusViewModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
