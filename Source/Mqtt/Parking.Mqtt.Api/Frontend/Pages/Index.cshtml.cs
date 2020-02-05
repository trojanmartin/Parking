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
    public class IndexModel : PageModel
    {
        private readonly Parking.Mqtt.Infrastructure.Data.ApplicationDbContext _context;

        public IndexModel(Parking.Mqtt.Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MqttServerConfiguration> MqttServerConfiguration { get;set; }



        public async Task OnGetAsync()
        {
            MqttServerConfiguration = await _context.MqttServerConfigurations.Include(x => x.Topics).ToListAsync();
        }
    }
}
