using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BullshitTracker.Startup))]
namespace BullshitTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
