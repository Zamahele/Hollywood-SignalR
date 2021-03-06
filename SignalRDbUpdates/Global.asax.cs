﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //Start SqlDependency with application initialization
            try
            {
                var dbContext = new ApplicationDbContext();
                if (dbContext.Database.Exists())
                    SqlDependency.Start(connString);
            }
            catch (Exception e)
            {
                
            }
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}
