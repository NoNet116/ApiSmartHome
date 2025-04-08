
namespace ApiSmartHome.Contracts.Models.Devices
{
    /// <summary>
    /// Запрос для обновления свойств подключенного устройства
    /// </summary>
    public class EditDeviceRequest
    {
        public Guid NewRoomId { get; set; }
        public string NewName { get; set; }
        public string NewSerial { get; set; }
    }
}
