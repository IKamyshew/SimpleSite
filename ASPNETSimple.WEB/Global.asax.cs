﻿using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ASPNETSimple.WEB.App_Start;

namespace ASPNETSimple
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            logger.Info("Application Start");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Init()
        {
            logger.Info("Application Init");
            base.Init();
        }

        public override void Dispose()
        {
            logger.Info("Application Dispose");
            base.Dispose();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            logger.Error(exception, "Application Error");
        }


        protected void Application_End()
        {
            logger.Info("Application End");
        }
    }
}
