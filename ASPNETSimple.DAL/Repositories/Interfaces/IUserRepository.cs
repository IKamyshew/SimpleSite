using System;
using System.Collections.Generic;
using ASPNETSimple.DAL.Entities;

namespace ASPNETSimple.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User Get(string login, string password);
        IEnumerable<User> Find(Func<User, Boolean> predicate);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
