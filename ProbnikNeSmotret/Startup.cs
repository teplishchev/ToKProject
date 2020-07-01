using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProbnikNeSmotret.Startup))]
namespace ProbnikNeSmotret
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
