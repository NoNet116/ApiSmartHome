using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiSmartHome.Data.Repository;
using ApiSmartHome.Contracts.Models.Rooms;
using ApiSmartHome.Data.Models;
using ApiSmartHome.Data.Queries;

namespace ApiSmartHome.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;

        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Info()
        {
            var rooms = await _repository.GetRooms();
            if (rooms == null)
            {
                return StatusCode(200, $"Нет зарегистрированных комнат.");
            }
            var resp = new GetRoomsResponse
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
            await _repository.AddRoom(newRoom);
           
            return Ok();
            
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRoom([FromRoute] Guid id, [FromBody] EditRoomRequest request)
        {
            var room = await _repository.GetRoomById(id);

            if (room == null)
                  return NotFound($"Комната с ID {id} не найдена.");
            

            // Обновляем свойства комнаты
            room.Name = request.NewName;
            room.Area = request.NewArea;
            room.GasConnected = request.NewGasConnected;
            room.Voltage = request.NewVoltage;

            try
            {
                await _repository.UpdateRoom(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при обновлении комнаты: {ex.Message}");
            }
            
            var resp = _mapper.Map<Room, RoomView>(room);
            return StatusCode(200, resp);
        }
    }
}
