using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDevFinal.Startup))]
namespace WebDevFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
