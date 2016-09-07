using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Premium_Web_Client.Startup))]
namespace Premium_Web_Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
