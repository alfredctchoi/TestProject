using TestProject.Database.Context;
using TestProject.Database.Context.Interface;
using TestProject.Repository;
using TestProject.Repository.Interface;
using TestProject.Service;
using TestProject.Service.Interface;

[assembly: WebActivator.PostApplicationStartMethod(typeof(TestProject.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace TestProject.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.RegisterWebApiRequest<IAdminContext, AdminContext>();
            container.RegisterWebApiRequest<IUserService, UserService>();
            container.RegisterWebApiRequest<ISessionService, SessionService>();
            container.RegisterWebApiRequest<IUserRepository, UserRepository>();
            container.RegisterWebApiRequest<ISessionRepository, SessionRepository>();

            // For instance:
            // container.RegisterWebApiRequest<IUserRepository, SqlUserRepository>();
        }
    }
}