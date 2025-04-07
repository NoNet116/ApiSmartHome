using ApiSmartHome.Data.Models;
using ApiSmartHome.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSmartHome.Data.Repository
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к объектам типа Device в базе 
    /// </summary>
    public interface IDeviceRepository
    {
        Task<Device?> GetDeviceBySN(string serialNumber);
        Task<Device?> GetDeviceById(Guid id);
        Task<Device[]> GetDevices();
        Task SaveDevice(Device device,Room room);
        Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query);

    }
}
