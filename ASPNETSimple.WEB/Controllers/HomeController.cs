using System.Collections.Generic;
using System.Web.Mvc;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Models;
using ASPNETSimple.BLL.Services.Interfaces;

namespace ASPNETSimple.WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService UserService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public HomeController(IUserService userService)
        {
            UserService = userService;
            logger.Info("Home controller created");
        }

        // GET: Home
        public ActionResult Home()
        {
            IEnumerable<UserModel> users;

            ServiceResult serviceResult = UserService.GetAllUsers(out users);

            return View(users);
        }
    }
}