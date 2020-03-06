using System;
using Microsoft.Owin;
using Owin;
using SignalRDbUpdates;
using SignalRDbUpdates.Models;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalRDbUpdates
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();

            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

        }



        //private static void UpdateDatabase(IAppBuilder app)
        //{
        //    using (var serviceScope = app.
        //        .GetRequiredService<IServiceScopeFactory>()
        //        .CreateScope())
        //    {
        //        using (var context = serviceScope.ServiceProvider.GetService<BookClubDbContext>())
        //        {
        //            try
        //            {
        //                context.Database.Migrate();
        //            }
        //            catch (Exception)
        //            {


        //            }
        //        }
        //    }
        //}
    }
}
