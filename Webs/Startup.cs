using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webs.Startup))]
namespace Webs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
