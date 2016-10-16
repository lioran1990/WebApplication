using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShaulisBlog.Startup))]
namespace ShaulisBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
