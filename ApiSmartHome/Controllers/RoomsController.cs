using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiSmartHome.Data.Repository;
using ApiSmartHome.Contracts.Models.Rooms;
using ApiSmartHome.Data.Models;

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
            return StatusCode(201, $"Комната {request.Name} добавлена!");
            
        }
    }
}
