using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sattelizer.Startup))]
namespace Sattelizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
