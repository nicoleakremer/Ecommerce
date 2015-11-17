using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookwormApp.Startup))]
namespace BookwormApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
