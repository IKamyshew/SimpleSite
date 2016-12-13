using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Interfaces;
using ASPNETSimple.BLL.Models;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace ASPNETSimple.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork UnitOfWork { get; set; }

        public UserService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public async Task<ServiceResult> Create(UserModel userModel)
        {
            ApplicationUser user = await UnitOfWork.UserManager.FindByEmailAsync(userModel.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userModel.Email, UserName = userModel.Email };
                var result = await UnitOfWork.UserManager.CreateAsync(user, userModel.Password);
                if (result.Errors.Count() > 0)
                    return new ServiceResult(result.Errors.FirstOrDefault());
                // добавляем роль
                await UnitOfWork.UserManager.AddToRoleAsync(user.Id, userModel.Role);
                // создаем профиль клиента
                UserProfile userProfile = new UserProfile { Id = user.Id, Address = userModel.Address, Name = userModel.Name };
                UnitOfWork.UserProfiles.Create(userProfile);
                await UnitOfWork.SaveAsync();
                return ServiceResult.Success;
            }
            else
            {
                return new ServiceResult("Пользователь с таким логином уже существует");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserModel userModel)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await UnitOfWork.UserManager.FindAsync(userModel.Email, userModel.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await UnitOfWork.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserModel adminModel, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await UnitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await UnitOfWork.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminModel);
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
