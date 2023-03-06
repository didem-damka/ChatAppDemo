using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRChatApplication.Models;
using SignalRChatApplication.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRedisCacheService _redisCacheManager;

        public HomeController(ILogger<HomeController> logger, IRedisCacheService redisCacheManager)
        {
            _logger = logger;
            _redisCacheManager = redisCacheManager;
        }
        public IActionResult Index()
        {
            //Check Redis
            var cacheKey = "Messages";
            var result = _redisCacheManager.Get<List<UserModel>>(cacheKey);
            if (result != null)
            {
                return View(result);
            }
            else
                return View();
          
        }
        [HttpPost]
        public IActionResult Index(List<UserModel> messages)
        {
            //Check Redis
            var cacheKey = "Messages";
            _redisCacheManager.Set(cacheKey, messages);
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
