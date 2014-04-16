namespace Personal.Website
{
    using System.Data.Entity;
    using Personal.Persistence;
    using Personal.ServiceLayer;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Unity.Mvc4;

    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, PersonalPersistenceContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IPostService, PostService>();
        }
    }
}