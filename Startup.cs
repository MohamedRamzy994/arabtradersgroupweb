using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArabTradersGroup.Startup))]
namespace ArabTradersGroup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
