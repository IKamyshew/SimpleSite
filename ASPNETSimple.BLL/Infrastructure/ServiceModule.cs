using ASPNETSimple.DAL.Infrastructure;
using ASPNETSimple.DAL.Interfaces;
using Ninject.Modules;

namespace ASPNETSimple.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
