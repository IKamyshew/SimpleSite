using System.Collections.Generic;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Models;

namespace ASPNETSimple.BLL.Services.Interfaces
{
    public interface IUserService
    {
        ServiceResult GetAllUsers(out IEnumerable<UserModel> users);
        ServiceResult GetUser(int id, out UserModel user);
        ServiceResult GetUser(string login, string password, out UserModel user);
        ServiceResult CreateUser(UserModel user);
    }
}
