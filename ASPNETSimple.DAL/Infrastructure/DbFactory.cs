using System.Configuration;
using ASPNETSimple.DAL.Context;
using ASPNETSimple.DAL.Interfaces;

namespace ASPNETSimple.DAL.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private EFContext _dbContext;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public EFContext GetInstance()
        {
            if (_dbContext != null) return _dbContext;
            else
            {
                _dbContext = new EFContext();
                _dbContext.Database.Log = logger.Warn;
                logger.Info("EF Context created.");
                return _dbContext;
            }
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
        
    }
}
