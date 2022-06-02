using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceBookStore.Web.Startup))]
namespace EcommerceBookStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
