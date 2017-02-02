using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElectronicRaffle.Web.Startup))]
namespace ElectronicRaffle.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
