using AutoMapper;
using ApiSmartHome.Contracts.Models.Home;
using ApiSmartHome.Contracts.Models.Devices;
using ApiSmartHome.Contracts.Models.Rooms;
using ApiSmartHome.Configuration;
using ApiSmartHome.Data.Models;
using ApiSmartHome.Contracts.Models.Users;



namespace ApiSmartHome.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Добавление всех маппингов
           
            AddRoomMappings();
            AddDeviceMappings();
            AddHomeOptionsMappings();
        }

        // Подключаем маппинг для Room
        private void AddRoomMappings()
        {
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Room, RoomView>();
        }

        // Подключаем маппинг для Device
        private void AddDeviceMappings()
        {
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location, opt => opt.MapFrom(r => r.RoomLocation));
            CreateMap<Device, DeviceView>();
        }

        // Подключаем маппинг для HomeOptions
        private void AddHomeOptionsMappings()
        {
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo, opt => opt.MapFrom(src => src.Address));

            CreateMap<Address, AddressInfo>();
        }

    }

}
