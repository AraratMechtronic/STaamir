using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Samane.Startup))]
namespace Samane
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
