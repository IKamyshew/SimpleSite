using System;
using System.Collections.Generic;
using ASPNETSimple.BLL.Infrastructure;
using ASPNETSimple.BLL.Models;
using ASPNETSimple.BLL.Services.Interfaces;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Interfaces;
using AutoMapper;
using System.Linq;

namespace ASPNETSimple.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork UnitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ServiceResult GetAllUsers(out IEnumerable<UserModel> users)
        {
            users = null;
            try
            {
                IEnumerable<User> entities = UnitOfWork.Users.GetAll();

                if (entities.Count() == 0)
                    return ServiceResult.Failed("There are no users in database");

                users = Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(entities);

                return ServiceResult.Success;
            }
            catch (Exception e)
            {
                return ServiceResult.Failed(e.Message);
            }
        }
    }
}
