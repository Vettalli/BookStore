using Microsoft.AspNetCore.Mvc;

namespace BookStore.YandexKass.Areas.YandexKassa.Controllers
{
    [Area("YandexKassa")]
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CallBack()
        {
            return View();
        }
    }
}
