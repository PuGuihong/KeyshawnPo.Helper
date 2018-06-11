using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KeyshawnPo.Demo.HtmlFactoryWeb.Startup))]
namespace KeyshawnPo.Demo.HtmlFactoryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
