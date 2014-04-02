using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Web.Mvc;
using Personal.ServiceLayer;
using Personal.Website.ViewModels;

namespace Personal.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        
        public HomeController(IPostService postService)
        {
            this._postService = postService;
        }

        public ActionResult Index()
        {
            var posts = this._postService.GetAllPosts();
            
            return this.View(posts);
        }

        public ActionResult Contact()
        {
            var viewModel = new ContactViewModel(string.Empty);

            return this.View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SendMessage(string email, string subject, string website, string message)
        {
            try
            {
                var mailMessage = new MailMessage(
                email,
                "info@jdibble.co.uk",
                string.Format(CultureInfo.CurrentCulture, @"Message from Contact Page: {0}", subject),
                string.Format(CultureInfo.CurrentCulture, @"Website: {0}<br /><br />Message:<br />{1}", website, message)) { IsBodyHtml = true };
                new SmtpClient().Send(mailMessage);
            }
            catch (Exception)
            {
                return new JsonResult
                {
                    Data = new Dictionary<string, object>() { { "success", false } },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new Dictionary<string, object>() { { "success", true } },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}