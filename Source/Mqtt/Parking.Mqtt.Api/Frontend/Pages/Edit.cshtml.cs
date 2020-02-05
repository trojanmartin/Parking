using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Api.Data;
using Parking.Mqtt.Api.Frontend.Pages.Models;

namespace Parking.Mqtt.Api
{
    public class EditModel : PageModel
    {
        private readonly Parking.Mqtt.Api.Data.ParkingMqttApiContext _context;

        public EditModel(Parking.Mqtt.Api.Data.ParkingMqttApiContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MqttStatusViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MqttStatusViewModelExists(MqttStatusViewModel.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MqttStatusViewModelExists(int id)
        {
            return _context.MqttStatusViewModel.Any(e => e.ID == id);
        }
    }
}
