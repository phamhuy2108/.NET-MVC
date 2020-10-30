using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAn1_DoAn.Startup))]
namespace DoAn1_DoAn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
