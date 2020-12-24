using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PAYROLLPORTAL.Startup))]
namespace PAYROLLPORTAL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
