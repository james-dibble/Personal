namespace Personal.Website
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Personal.Website.Handlers;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new Route("metaweblog", new MetaWeblogHandler()));
                        
            routes.MapRoute(
                "Contact",
                "contact",
                new {controller = "Home", action = "Contact"});

            routes.MapRoute(
                "BlogArchive",
                "blog",
                new { controller = "Posts", action = "BlogArchive", year = 0, month = 0 });

            routes.MapRoute(
                "BlogArchiveTag",
                "blog/tag/{tag}",
                new { controller = "Posts", action = "BlogArchiveTag", year = 0, month = 0 });

            routes.MapRoute(
                "BlogArchiveYear",
                "blog/{year}",
                new { controller = "Posts", action = "BlogArchive", month = 0 },
                new { year = @"^\d{4}$" });

            routes.MapRoute(
                "BlogArchiveMonth",
                "blog/{month}",
                new { controller = "Posts", action = "BlogArchive", year = 0 },
                new { month = @"^\d{1,2}$" });

            routes.MapRoute(
                "BlogArchiveYearMonth",
                "blog/{year}/{month}",
                new { controller = "Posts", action = "BlogArchive" },
                new { year = @"^\d{4}$", month = @"^\d{1,2}$" });
            
            routes.MapRoute(
                 "PortfolioArchive",
                 "portfolio",
                 new { controller = "Posts", action = "PortfolioArchive" });

            routes.MapRoute(
                 "PortfolioArchiveTag",
                 "portfolio/tag/{tag}",
                 new { controller = "Posts", action = "PortfolioArchiveTag" });

            routes.MapRoute(
                "Blog",
                "blog/{year}/{month}/{day}/{title}",
                new {controller = "Posts", action = "Blog"},
                new { year = @"^\d{4}$", month = @"^\d{1,2}$", day = @"^\d{1,2}$" });

            routes.MapRoute(
                "Portfolio",
                "portfolio/{year}/{month}/{day}/{title}",
                new { controller = "Posts", action = "Portfolio" },
                new { year = @"^\d{4}$", month = @"^\d{1,2}$", day = @"^\d{1,2}$" });

            routes.MapRoute(
                "LegacyBlog",
                "blog/{id}/{title}",
                new { controller = "Posts", action = "LegacyBlog" });

            routes.MapRoute(
                "LegacyPortfolio",
                "portfolio/{id}/{title}",
                new { controller = "Posts", action = "LegacyPortfolio" });
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}