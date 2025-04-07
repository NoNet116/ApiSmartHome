using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiSmartHome.Data.Repository;
using ApiSmartHome.Data.Models;
using ApiSmartHome.Contracts.Models.Users;
using ApiSmartHome.Contracts.Models.Devices;
using Microsoft.Extensions.Options;
using ApiSmartHome.Configuration;

namespace ApiSmartHome.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private IMapper _mapper;
        private IUserRepository _user;

        public UsersController(
            ILogger<UsersController> logger, 
            IMapper mapper, 
            IUserRepository users)
        {
            _logger = logger;
            _mapper = mapper;
            _user = users;
        }

        [HttpGet]
        [Route("")]
        public async Task <IActionResult> Info()
        {
            var users = await _user.GetUsers();
            if (users == null)
            {
                return NotFound($"Нет зарегистрированных пользователей.");
            }
            var resp = new GetUserResponse
            {
                UserAmount = users.Length,
                Users = _mapper.Map<Data.Models.User[], UserView[]>(users)
            };

            return StatusCode(200, resp);
        }


    }
}
