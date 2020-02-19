using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Database;
using Parking.Database.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api
{
    public class EditTopicModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditTopicModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MqttTopicConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MqttTopicConfigurationExists(MqttTopicConfiguration.Id))
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

        private bool MqttTopicConfigurationExists(int id)
        {
            return _context.MqttTopicConfigurations.Any(e => e.Id == id);
        }
    }
}
