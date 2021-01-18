using Owin;
using University.BL.Data;

namespace University.API
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //Configura el db context para que sea usado como unica instancia por request
            app.CreatePerOwinContext(UniversityContext.Create);
        }
    }
}
