using Microsoft.AspNetCore.Mvc;

namespace Tribulus.MarketPlace.Admin.Marketing.HttpApi.Host.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
