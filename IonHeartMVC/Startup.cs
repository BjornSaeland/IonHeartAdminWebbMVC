using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IonHeartMVC.Startup))]
namespace IonHeartMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
