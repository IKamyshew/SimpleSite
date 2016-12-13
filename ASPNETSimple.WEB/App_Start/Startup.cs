using ASPNETSimple.BLL.Interfaces;
using ASPNETSimple.BLL.Services;
using ASPNETSimple.DAL.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ASPNETSimple.WEB.App_Start.Startup))]

namespace ASPNETSimple.WEB.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return new UserService(new EFUnitOfWork(new DbFactory()));
        }
    }
}