using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Interfaces;

namespace ASPNETSimple.DAL.Repositories
{
    public class UserRepository : IRepository<User>
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

        public User Get(string id)
        {
            return db.Users.Find(id);
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

        public void Delete(string id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
