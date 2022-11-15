using Microsoft.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Inventory.HttpApi.Host.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
