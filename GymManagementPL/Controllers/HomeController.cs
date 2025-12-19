using Microsoft.AspNetCore.Mvc;
using GymManagementBL.Service.Interfaces;
using GymManagementDL.Entities;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;
        public HomeController(IAnalyticsService analyticsService)
        {
           _analyticsService = analyticsService;
        }
        public ActionResult Index()
        {
            var Data = _analyticsService.GetAllAnalytics();
            return View(Data);
        }
    }
}
