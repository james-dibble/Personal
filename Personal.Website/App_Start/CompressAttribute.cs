// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompressAttribute.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Personal.Website
{
    using System.IO.Compression;
    using System.Web.Mvc;

    public class CompressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            var response = filterContext.HttpContext.Response;

            if (string.IsNullOrEmpty(encodingsAccepted) || response.Filter == null) return;

            encodingsAccepted = encodingsAccepted.ToLowerInvariant();

            if (encodingsAccepted.Contains("gzip"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (encodingsAccepted.Contains("deflate"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}