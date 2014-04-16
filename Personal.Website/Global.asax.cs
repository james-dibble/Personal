namespace Personal.Website
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Bootstrapper.Initialise();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            const string properUrl = "http://www.jdibble.co.uk";
            const string nonWwwUrl = "http://jdibble.co.uk";

            string currentUrl = HttpContext.Current.Request.Url.ToString().ToLower();

            if (currentUrl.StartsWith(nonWwwUrl))
            {
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", properUrl);
                Response.End();
            }
        }
    }
}