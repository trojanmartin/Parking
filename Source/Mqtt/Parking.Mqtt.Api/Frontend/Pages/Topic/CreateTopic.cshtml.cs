using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parking.Mqtt.Infrastructure.Data;
using Parking.Mqtt.Infrastructure.Data.Entities;

namespace Parking.Mqtt.Api
{
    public class CreateTopicModel : PageModel
    {
        private readonly Parking.Mqtt.Infrastructure.Data.ApplicationDbContext _context;

        public CreateTopicModel(Parking.Mqtt.Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MqttTopicConfiguration MqttTopicConfiguration { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MqttTopicConfigurations.Add(MqttTopicConfiguration);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
