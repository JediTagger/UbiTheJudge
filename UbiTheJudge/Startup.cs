using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UbiTheJudge.Startup))]
namespace UbiTheJudge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
