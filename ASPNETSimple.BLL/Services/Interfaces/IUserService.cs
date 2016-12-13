using System.Collections.Generic;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Models;

namespace ASPNETSimple.BLL.Services.Interfaces
{
    public interface IUserService
    {
        ServiceResult GetAllUsers(out IEnumerable<UserModel> users);
    }
}
