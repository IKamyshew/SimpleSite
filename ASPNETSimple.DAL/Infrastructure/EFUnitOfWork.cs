using System;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Interfaces;
using ASPNETSimple.DAL.Repositories;
using ASPNETSimple.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace ASPNETSimple.DAL.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork
    {
        #region Init
        private readonly IDbFactory dbFactory;
        private EFContext db;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        private UserRepository userRepository;
        private IRepository<UserProfile> userProfileRepository;

        public EFUnitOfWork(IDbFactory dbFactory)
        {
            logger.Warn("UnitOfWork is created.");
            this.dbFactory = dbFactory;
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(DbContext));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(DbContext));
        }
        #endregion

        #region Getters
        public EFContext DbContext
        {
            get { return db ?? (db = dbFactory.GetInstance()); }
        }
        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        #region Repositories
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(DbContext);
                return userRepository;
            }
        }

        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                if (userProfileRepository == null)
                    userProfileRepository = new UserProfileRepository(DbContext);
                return userProfileRepository;
            }
        }
        #endregion

        #endregion

        #region Operations
        public void Save()
        {
            DbContext.SaveChanges();
            /*
            try
            {
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
            */
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        #endregion

        #region Dispose

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
                logger.Warn("Unit Disposed.");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    /*
    public class FormattedDbEntityValidationException : Exception
    {
        public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
            base(null, innerException)
        {
        }

        public override string Message
        {
            get
            {
                var innerException = InnerException as DbEntityValidationException;
                if (innerException != null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine();
                    sb.AppendLine();
                    foreach (var eve in innerException.EntityValidationErrors)
                    {
                        sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage));
                        }
                    }
                    sb.AppendLine();

                    return sb.ToString();
                }

                return base.Message;
            }
        }
    }
    */
}