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
    public class DeleteModel : PageModel
    {
        private readonly Parking.Mqtt.Api.Data.ParkingMqttApiContext _context;

        public DeleteModel(Parking.Mqtt.Api.Data.ParkingMqttApiContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MqttStatusViewModel = await _context.MqttStatusViewModel.FindAsync(id);

            if (MqttStatusViewModel != null)
            {
                _context.MqttStatusViewModel.Remove(MqttStatusViewModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
