using ASPNETSimple.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace ASPNETSimple.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
