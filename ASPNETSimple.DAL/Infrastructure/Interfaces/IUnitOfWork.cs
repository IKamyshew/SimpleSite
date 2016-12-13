using System;
using System.Threading.Tasks;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Identity;

namespace ASPNETSimple.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        
        IRepository<User> Users { get; }
        IRepository<UserProfile> UserProfiles { get; }
    }
}
