[assembly: WebActivator.PostApplicationStartMethod(typeof(WebResults.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace WebResults.App_Start
{
    using System.Web.Http;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Dao.ResultCtx;
    using Service;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IResultDataAccess, ResultDataAccess>(Lifestyle.Scoped);
            container.Register<ResultContext>(Lifestyle.Scoped);
            container.Register<ResultMapper>(Lifestyle.Scoped);
            container.Register<ResultService>(Lifestyle.Scoped);
        }
    }
}