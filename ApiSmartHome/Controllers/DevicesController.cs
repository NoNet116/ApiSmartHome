using ApiSmartHome.Contracts.Models.Devices;
using ApiSmartHome.Contracts.Models.Rooms;
using ApiSmartHome.Data.Models;
using ApiSmartHome.Data.Queries;
using ApiSmartHome.Data.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSmartHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private IDeviceRepository _devices;
        private IRoomRepository _rooms;
        private IMapper _mapper;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(IDeviceRepository devices, IRoomRepository rooms, IMapper mapper, ILogger<DevicesController> logger)
        {
            _devices = devices;
            _rooms = rooms;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Info()
        {
            var devices = await _devices.GetDevices();
            if (devices == null)
            {
                return StatusCode(200, $"Нет зарегистрированных устройств.");
            }
            var resp = new GetDevicesResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(AddDeviceRequest request)
        {
            var room = await _rooms.GetRoomById(request.RoomId);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceBySN(request.SerialNumber);
            if (device != null)
                return StatusCode(400, $"Ошибка: Устройство с серийным номером {request.SerialNumber} уже существует.");

            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);

            await _devices.SaveDevice(newDevice, room);

            return StatusCode(201, $"Устройство {request.Name} добавлено. Идентификатор: {newDevice.Id}");
        }

        /// <summary>
        /// Обновление существующего устройства
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody] EditDeviceRequest request)
        {
            var room = await _rooms.GetRoomById(request.NewRoomId);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceById(id);
            if (device == null)
                return StatusCode(400, $"Ошибка: Устройство с идентификатором {id} не существует.");

            var withSameName = await _devices.GetDeviceBySN(request.NewSerial);
            if (withSameName != null)
                return StatusCode(400, $"Ошибка: Устройство с серийным номером {request.NewSerial} уже существует.");

            await _devices.UpdateDevice(device, room, new UpdateDeviceQuery(request.NewName, request.NewSerial)
            );

            return StatusCode(200, $"Устройство обновлено! Имя - {device.Name}, Серийный номер - {device.SerialNumber},  Комната подключения - {device.Room.Name}");
        }

    }
}
