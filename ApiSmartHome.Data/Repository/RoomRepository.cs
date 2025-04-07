using ApiSmartHome.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSmartHome.Data.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;

        public RoomRepository(HomeApiContext context) => _context = context;

        
        public async Task AddRoom(Room  room)
        {
            ArgumentNullException.ThrowIfNull(room);

            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);

            await _context.SaveChangesAsync();
        }

        public async  Task<Room?> GetRoomById(Guid id)
        {
            return await _context.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        ///  Выгрузить все комнаты
        /// </summary>
        public async Task<Room[]> GetRooms()
        {
            return await _context.Rooms.ToArrayAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            ArgumentNullException.ThrowIfNull(room);

            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }
    }
}
