using System;
using ASPNETSimple.DAL.Entities;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Interfaces;
using ASPNETSimple.DAL.Repositories;

namespace ASPNETSimple.DAL.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IDbFactory dbFactory;
        private readonly string connectionString;
        private EFContext db;

        private UserRepository userRepository;

        public EFUnitOfWork(string connectionString)
        {
            this.connectionString = connectionString;
            dbFactory = new DbFactory();
            logger.Warn("EFUnitOfWork created.");
        }

        public EFContext DbContext
        {
            get { return db ?? (db = dbFactory.GetInstance(connectionString)); }
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