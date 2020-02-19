using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Database;
using Parking.Database.Entities;
using Parking.Mqtt.Core.Exceptions;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT;
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
            var res = await _context.MqttServerConfigurations.Include(x => x.Topics).ToListAsync();

            return new List<MQTTServerConfigurationDTO>(_mapper.Map<ICollection<MqttServerConfiguration>, IEnumerable<MQTTServerConfigurationDTO>>(res));
        }

        public async Task<MQTTServerConfigurationDTO> GetConfigurationAsync(int id)
        {
            var config = await _context.MqttServerConfigurations.FindAsync(id);

            if (config == null)
                throw new NotFoundException($"Configuration with id {id} not found");

            return _mapper.Map<MQTTServerConfigurationDTO>(config);
        }



        public async Task CreateConfigurationAsync(MQTTServerConfigurationDTO configuration)
        {
            await _context.MqttServerConfigurations.AddAsync(_mapper.Map<MqttServerConfiguration>(configuration));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateConfigurationAsync(MQTTServerConfigurationDTO configuration)
        {
            var toUpdate = await _context.MqttServerConfigurations.FindAsync(configuration.Id);

            if (toUpdate != null)
            {
                _mapper.Map(configuration,toUpdate);                
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException($"Configuration with id {configuration.Id} not found");
        }
    }
}
