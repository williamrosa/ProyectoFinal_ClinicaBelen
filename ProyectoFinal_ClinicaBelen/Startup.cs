using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoFinal_ClinicaBelen.Startup))]
namespace ProyectoFinal_ClinicaBelen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
