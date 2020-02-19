using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Database.Entities;
using Parking.Database;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MqttServerConfiguration MqttServerConfiguration { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MqttServerConfigurations.Add(MqttServerConfiguration);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
