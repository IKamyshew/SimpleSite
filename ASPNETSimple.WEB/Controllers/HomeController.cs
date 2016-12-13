using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Infrastructure;

namespace ASPNETSimple.WEB.Controllers
{
    public class HomeController : Controller
    {
        private EFUnitOfWork DbContext;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public HomeController()
        {
            DbContext = new EFUnitOfWork(ConfigurationManager.ConnectionStrings["EFContext"].ConnectionString);
            logger.Info("Home controller created");
        }

        // GET: Home
        public ActionResult Home()
        {
            IEnumerable<User> users = DbContext.Users.GetAll();
            return View(users);
        }
    }
}