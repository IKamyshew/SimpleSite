using System.Collections.Generic;
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
            logger.Info("Home controller created");
            DbContext = new EFUnitOfWork(new DbFactory());
        }

        // GET: Home
        public ActionResult Home()
        {
            IEnumerable<UserProfile> profiles = DbContext.UserProfiles.GetAll();
            return View(profiles);
        }
        
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}