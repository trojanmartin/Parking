using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Database;
using Parking.Database.Entities;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class CreateTopicModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateTopicModel(ApplicationDbContext context)
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
