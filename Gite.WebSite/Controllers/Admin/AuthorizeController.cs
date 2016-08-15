using System.Web.Mvc;

namespace Gite.WebSite.Controllers.Admin
{
    [Authorize]
    public abstract class AuthorizeController : Controller
    {
    }
}