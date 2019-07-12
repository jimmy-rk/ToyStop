using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceSite.Web.Startup))]
namespace EcommerceSite.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
