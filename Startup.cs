using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroupBuy1.Startup))]
namespace GroupBuy1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
