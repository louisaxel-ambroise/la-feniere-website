using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    public class CalendarController : AuthorizeController
    {
        public ActionResult Index()
        {
            return View("~/Views/Admin/Calendar/Index.cshtml");
        }
    }
}