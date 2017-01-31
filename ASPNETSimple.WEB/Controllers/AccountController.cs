using System.Web.Mvc;
using System.Linq;
using System.Web.Security;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Models;
using ASPNETSimple.BLL.Services.Interfaces;
using ASPNETSimple.WEB.Models.Account;
using AutoMapper;

namespace ASPNETSimple.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserModel user = null;
                ServiceResult serviceResult = UserService.GetUser(viewModel.Email, viewModel.Password, out user);
                if (serviceResult.Succeeded && user != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, true);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    foreach (var error in serviceResult.Errors)
                        logger.Error(error);

                    ModelState.AddModelError("", "Such user does not exist");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserModel model = Mapper.Map<RegisterViewModel, UserModel>(viewModel);

                ServiceResult serviceResult = UserService.CreateUser(model);

                if (serviceResult.Succeeded)
                {
                    TempData["Notification"] = "User have been successfully created";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in serviceResult.Errors)
                        logger.Error(error);

                    ModelState.AddModelError("", "Some information is not valid");
                }
            }

            return View(viewModel);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Home", "Home");
        }
    }
}