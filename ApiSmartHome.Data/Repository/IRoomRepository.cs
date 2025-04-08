using ApiSmartHome.Data.Models;

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
