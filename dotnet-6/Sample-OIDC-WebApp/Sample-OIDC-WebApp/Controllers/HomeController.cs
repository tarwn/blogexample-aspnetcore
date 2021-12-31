using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample_OIDC_WebApp.Configuration;
using Sample_OIDC_WebApp.Models;

namespace Sample_OIDC_WebApp.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new HomeModel(User);
            return View(model);
        }

        [HttpGet("/protected")]
        [Authorize(Policies.InteractiveUser)]
        public ActionResult Protected()
        {
            var model = new HomeModel(User);
            return View("Index", model);
        }

        [HttpGet("/missingAuth")]
        public ActionResult SampleEndpointWithoutExplicitAuthorization()
        {
            var model = new HomeModel(User);
            return View("Index", model);
        }
    }
}
