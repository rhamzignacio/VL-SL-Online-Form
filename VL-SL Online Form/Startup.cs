using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VL_SL_Online_Form.Startup))]
namespace VL_SL_Online_Form
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
