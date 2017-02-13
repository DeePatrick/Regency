using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Regency.Startup))]
namespace Regency
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
