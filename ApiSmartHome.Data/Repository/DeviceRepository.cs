using ApiSmartHome.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiSmartHome.Data.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HomeApiContext _context;

        public DeviceRepository(HomeApiContext context)
        {
            _context = context;
        }

        public async Task<Device?> GetDeviceBySN(string serialNumber)
        {
            return await _context.Devices
            .Include(d => d.Room)
                .Where(d => d.SerialNumber == serialNumber).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Выгрузить все устройства
        /// </summary>
        public async Task<Device[]> GetDevices()
        {
            return await _context.Devices
                .Include(d => d.Room)
                .ToArrayAsync();
        }

        /// <summary>
        /// Добавить новое устройство
        /// </summary>
        public async Task SaveDevice(Device device, Room room)
        {
            // Привязываем новое устройство к соответствующей комнате перед сохранением
            device.RoomId = room.Id;
            device.Room = room;

            // Добавляем в базу 
            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                await _context.Devices.AddAsync(device);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }
    }
}
