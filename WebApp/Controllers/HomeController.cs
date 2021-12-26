using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApp.AppCode;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor _accessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            return Redirect("/en");

            if (_accessor.HttpContext.Connection.RemoteIpAddress.ToString() == "::1") return Redirect("/fa");
            var ipChecker = new CountryIPRanges().GetCountry(_accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            switch (ipChecker)
            {
                case "Iran":
                    return Redirect("/fa");
                case "Turkey":
                    return Redirect("/tr");
                default:
                    return Redirect("/en");
            }
        }

        public IActionResult ChangeLanguage(string ln)
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo(ln);
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo(ln);
            //Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(ln)),
            //    new CookieOptions() { Expires = DateTime.Now.AddDays(1) });
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer);
            return Redirect("/");
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
