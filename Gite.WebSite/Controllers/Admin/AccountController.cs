using System.Web.Mvc;
using System.Web.Security;
using Gite.WebSite.Models.Admin;
using System;

namespace Gite.WebSite.Controllers.Admin
{
    public class AccountController : Controller
    {
        private readonly Credentials _adminCredentials;

        public AccountController(Credentials adminCredentials)
        {
            if (adminCredentials == null) throw new ArgumentException("adminCredentials");

            _adminCredentials = adminCredentials;
        }

        public ActionResult Index()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            return View("~/Views/Admin/Account/Index.cshtml", new Credentials());
        }

        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            return View("~/Views/Admin/Account/Index.cshtml", new Credentials());
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return Redirect("/admin");
        }

        [HttpPost]
        public ActionResult Login(Credentials credentials)
        {
            if (credentials.Username == _adminCredentials.Username && credentials.Password == _adminCredentials.Password)
            {
                FormsAuthentication.SetAuthCookie("someUser", true);

                return Redirect(Request.QueryString["ReturnUrl"] ?? "/admin");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}