using ApiSmartHome.Configuration;
using ApiSmartHome.Contracts.Models.Home;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiSmartHome.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;
        private IOptions<HomeOptions> _options;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IOptions<HomeOptions> options)
        {
            _logger = logger;
            _mapper = mapper;
            _options = options;
        }

        [HttpGet]
        [Route("HelloWorld")]
        public IActionResult HW()
        {
            var requestPath = HttpContext.Request.Path;
            var requestMethod = HttpContext.Request.Method;
            var requestTime = DateTime.Now;
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();

            var info = $"Вызван метод: {requestMethod}. URL: {requestPath}. Время: {requestTime}. IP клиента: {clientIp}";
            // Логируем информацию о запросе
            _logger.LogInformation(info);
            return StatusCode(200, info);
        }

        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        [HttpGet]
        [Route("info")]
        public IActionResult Info()
        {
            var infoResponse = _mapper.Map<HomeOptions, Contracts.Models.Home.InfoResponse>(_options.Value);

            return StatusCode(200, infoResponse);
        }

    }
}
