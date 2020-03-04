using Microsoft.Owin;
using Owin;
using SignalRDbUpdates;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalRDbUpdates
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();   
        }
    }
}
