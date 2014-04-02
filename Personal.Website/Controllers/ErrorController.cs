// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Personal.Website.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Error");
        }

        public ActionResult NotFound()
        {
            return this.View("NotFound");
        }
    }
}