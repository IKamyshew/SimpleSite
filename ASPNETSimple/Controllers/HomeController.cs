using System.Collections.Generic;
using System.Web.Mvc;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Infrastructure;
using ASPNETSimple.DAL.Repositories;

namespace ASPNETSimple.WEB.Controllers
{
    public class HomeController : Controller
    {
        private EFUnitOfWork DbContext;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public HomeController()
        {
            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");
            DbContext = new EFUnitOfWork(new DbFactory());
        }

        // GET: Home
        public ActionResult Home()
        {
            IEnumerable<User> users = DbContext.Users.GetAll();
            return View(users);
        }
    }
}