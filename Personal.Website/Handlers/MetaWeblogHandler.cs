namespace Personal.Website.Handlers
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Personal.ServiceLayer;

    public class MetaWeblogHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MetaWeblogService();
        }
    }
}