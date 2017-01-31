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
        private IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceResult GetAllUsers(out IEnumerable<UserModel> users)
        {
            users = null;
            try
            {
                IEnumerable<User> entities = unitOfWork.Users.GetAll();

                if (entities.Count() == 0)
                    return ServiceResult.Success;

                users = Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(entities);

                return ServiceResult.Success;
            }
            catch (Exception e)
            {
                return ServiceResult.Failed(e.Message);
            }
        }

        public ServiceResult GetUser(int id, out UserModel user)
        {
            user = null;
            try
            {
                User entity = unitOfWork.Users.Get(id);

                if (entity == null)
                    return ServiceResult.Success;

                user = Mapper.Map<User, UserModel>(entity);

                return ServiceResult.Success;
            }
            catch (Exception e)
            {
                return ServiceResult.Failed(e.Message);
            }
        }
        public ServiceResult GetUser(string login, string password, out UserModel user)
        {
            user = null;
            try
            {
                User entity = unitOfWork.Users.Get(login, password);

                if (entity == null)
                    return ServiceResult.Success;

                user = Mapper.Map<User, UserModel>(entity);

                return ServiceResult.Success;
            }
            catch (Exception e)
            {
                return ServiceResult.Failed(e.Message);
            }
        }

        public ServiceResult CreateUser(UserModel user)
        {
            try
            {
                if (user == null)
                    return ServiceResult.Failed("Cannot create null user.");

                User entity = Mapper.Map<UserModel, User> (user);

                unitOfWork.Users.Create(entity);
                unitOfWork.Save();

                return ServiceResult.Success;
            }
            catch (Exception e)
            {
                return ServiceResult.Failed(e.Message);
            }
        }
    }
}
