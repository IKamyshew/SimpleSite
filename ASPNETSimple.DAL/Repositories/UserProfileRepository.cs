using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Interfaces;

namespace ASPNETSimple.DAL.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private EFContext db;

        public UserProfileRepository(EFContext context)
        {
            this.db = context;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles;
        }

        public UserProfile Get(string id)
        {
            return db.UserProfiles.Find(id);
        }

        public void Create(UserProfile user)
        {
            db.UserProfiles.Add(user);
        }

        public void Update(UserProfile user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, Boolean> predicate)
        {
            return db.UserProfiles.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            UserProfile user = db.UserProfiles.Find(id);
            if (user != null)
                db.UserProfiles.Remove(user);
        }
    }
}
