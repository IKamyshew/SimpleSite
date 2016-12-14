using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Repositories.Interfaces;

namespace ASPNETSimple.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private EFContext db;

        public UserRepository(EFContext context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public User Get(string login, string password)
        {
            return db.Users
                .Where(user => user.Email.Equals(login) && user.Password.Equals(password))
                .FirstOrDefault();
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
