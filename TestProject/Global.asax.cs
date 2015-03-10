using System.Web;
using System.Web.Http;
using TestProject.Filters;

namespace TestProject
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new GlobalException());
        }
    }
}
