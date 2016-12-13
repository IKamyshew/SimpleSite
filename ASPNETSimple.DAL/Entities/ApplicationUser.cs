using Microsoft.AspNet.Identity.EntityFramework;

namespace ASPNETSimple.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfile ClientProfile { get; set; }
    }
}
