using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlantTracker.Startup))]
namespace PlantTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
