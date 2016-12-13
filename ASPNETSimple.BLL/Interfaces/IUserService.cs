using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Models;

namespace ASPNETSimple.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<ServiceResult> Create(UserModel userModel);
        Task<ClaimsIdentity> Authenticate(UserModel userModel);
        Task SetInitialData(UserModel adminModel, List<string> roles);
    }
}
