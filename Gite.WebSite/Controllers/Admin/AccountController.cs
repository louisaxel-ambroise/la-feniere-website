using Gite.WebSite.Models.Admin;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gite.WebSite.Controllers.Admin
{
    public class AccountController : Controller
    {
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
            if (credentials.Username == "admin" && credentials.Password == "admin")
            {
                FormsAuthentication.SetAuthCookie("someUser", true);

                return Redirect(Request.QueryString["ReturnUrl"] ?? "/admin");
            }
            else
            {
                return View("~/Views/Admin/Account/Index.cshtml", credentials);
            }
        }
    }
}