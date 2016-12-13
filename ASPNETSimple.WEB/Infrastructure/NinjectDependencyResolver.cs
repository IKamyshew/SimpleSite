using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ASPNETSimple.BLL.Services;
using ASPNETSimple.BLL.Services.Interfaces;
using Ninject;

namespace ASPNETSimple.WEB.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}