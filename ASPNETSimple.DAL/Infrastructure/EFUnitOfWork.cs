using System;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Interfaces;
using ASPNETSimple.DAL.Repositories;

namespace ASPNETSimple.DAL.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private EFContext db;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private UserRepository userRepository;

        public EFUnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public EFContext DbContext
        {
            get { return db ?? (db = dbFactory.GetInstance()); }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(DbContext);
                return userRepository;
            }
        }

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
        /*
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
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        */
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