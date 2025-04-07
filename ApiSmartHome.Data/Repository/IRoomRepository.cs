using ApiSmartHome.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSmartHome.Data.Repository
{
    public interface IRoomRepository
    {
        Task<Room[]> GetRooms();
        Task AddRoom(Room room);
        Task<Room?> GetRoomById(Guid id);
        Task UpdateRoom(Room room);
    }
}
