using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Data.Repositories
{
    public class MqttConfigurationRepo : IMQTTConfigurationRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MqttConfigurationRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrUpdateMqttStatusAsync(MqttServerConfiguration configuration)
        {
            var conf = await _context.MqttServerConfigurations.FindAsync(configuration?.Id);

            if (conf == null)
            {
                await _context.MqttServerConfigurations.AddAsync(_mapper.Map<MqttServerConfiguration>(configuration));

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                conf = _mapper.Map<MqttServerConfiguration>(configuration);

                _context.Update(conf);

                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<IEnumerable<MQTTServerConfigurationDTO>> GetConfigurationsAsync()
        {
            var res = await  _context.MqttServerConfigurations.Include(x => x.Topics).ToListAsync();

            return new List<MQTTServerConfigurationDTO>( _mapper.Map<ICollection<MqttServerConfiguration>,IEnumerable<MQTTServerConfigurationDTO>>(res));
        }

        public async Task<MQTTServerConfigurationDTO> GetConfigurationAsync(string id)
        {

            var config = await _context.MqttServerConfigurations.FindAsync(id);

            return _mapper.Map<MQTTServerConfigurationDTO>(config);
        }

        public Task<bool> CreateOrUpdateMqttStatusAsync(MQTTServerConfigurationDTO configuration)
        {
            throw new System.NotImplementedException();
        }

        public Task<MQTTServerConfigurationDTO> GetConfigurationAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreateConfigurationAsync(MQTTServerConfigurationDTO configuration)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateConfigurationAsync(MQTTServerConfigurationDTO configuration)
        {
            throw new System.NotImplementedException();
        }
    }
}
