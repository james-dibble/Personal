using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Web.Mvc;
using Personal.Website.ViewModels;

namespace Personal.Website.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 604800)]
        public ActionResult Index()
        {
            return this.View();
        }

        [OutputCache(Duration = 604800)]
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
                "mailer@jdibble.co.uk",
                "info@jdibble.co.uk",
                string.Format(CultureInfo.CurrentCulture, @"Message from Contact Page: {0}", subject),
                string.Format(CultureInfo.CurrentCulture, @"Return Address: {2}<br /><br />Website: {0}<br /><br />Message:<br />{1}", website, message, email))
                {
                    IsBodyHtml = true
                };

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