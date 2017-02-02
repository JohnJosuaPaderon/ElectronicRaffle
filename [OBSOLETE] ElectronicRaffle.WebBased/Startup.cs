using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElectronicRaffle.WebBased.Startup))]
namespace ElectronicRaffle.WebBased
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
