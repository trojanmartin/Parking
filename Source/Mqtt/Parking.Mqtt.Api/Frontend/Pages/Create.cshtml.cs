using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parking.Mqtt.Api.Data;
using Parking.Mqtt.Api.Frontend.Pages.Models;

namespace Parking.Mqtt.Api
{
    public class CreateModel : PageModel
    {
        private readonly Parking.Mqtt.Api.Data.ParkingMqttApiContext _context;

        public CreateModel(Parking.Mqtt.Api.Data.ParkingMqttApiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MqttStatusViewModel MqttStatusViewModel { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MqttStatusViewModel.Add(MqttStatusViewModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
