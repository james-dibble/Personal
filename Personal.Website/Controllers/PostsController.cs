namespace Personal.Website.Controllers
{
    using System;
    using System.Web.Mvc;

    using Personal.DomainModel;
    using Personal.ServiceLayer;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Personal.Website.ViewModels;

    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            this._postService = postService;
        }

        public ActionResult LegacyBlog(int id, string title)
        {
            var blog = this._postService.GetBlog(title.Replace('-', ' '));

            if (blog == null)
            {
                return this.RedirectToAction("notfound", "error");
            }

            return this.RedirectToActionPermanent(
                "blog",
                "posts",
                new
                {
                    year = blog.Date.Year,
                    month = blog.Date.Month,
                    day = blog.Date.Day,
                    title = blog.Title.ToLowerInvariant().Replace(' ', '-')
                });
        }

        public ActionResult LegacyPortfolio(int id, string title)
        {
            var portfolio = this._postService.GetPortfolio(title.Replace('-', ' '));

            if (portfolio == null)
            {
                return this.RedirectToAction("notfound", "error");
            }

            return this.RedirectToActionPermanent(
                "portfolio",
                "posts",
                new
                {
                    year = portfolio.Date.Year,
                    month = portfolio.Date.Month,
                    day = portfolio.Date.Day,
                    title = portfolio.Title.ToLowerInvariant().Replace(' ', '-')
                });
        }

        public ActionResult BlogArchive(int year, int month)
        {
            var blogs = this._postService.GetBlogs(year, month).OrderByDescending(b => b.Date);

            var viewModel = new BlogHeaderViewModel { Year = year, Month = month, Blogs = blogs };

            return this.View(viewModel);
        }

        public ActionResult BlogHeader(int year = 0, int month = 0, bool canRemove = true)
        {
            var blogs = this._postService.GetAllBlogs();

            var viewModel = new BlogHeaderViewModel { Year = year, Month = month, Blogs = blogs, CanRemove = canRemove };

            return this.PartialView(viewModel);
        }

        public ActionResult Blog(int year, int month, int day, string title)
        {
            var blog = this._postService.GetBlog(new DateTime(year, month, day), title.Replace('-', ' '));

            if (blog == null)
            {
                return this.RedirectToAction("notfound", "error");
            }

            return this.View(blog);
        }

        public ActionResult WriteBlog(int id = 0)
        {
            if (id == 0)
            {
                return this.View(new Blog());
            }

            var blog = this._postService.GetBlog(id);

            return this.View(blog);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult WriteBlog(Blog blog)
        {
            var savedBlog = this._postService.SaveBlog(blog);

            return this.RedirectToAction("WriteBlog", "Posts", new { id = savedBlog.Id });
        }

        public ActionResult PortfolioArchive()
        {
            var portfolios = this._postService.GetAllPortfolios().OrderByDescending(p => p.Date); ;

            return this.View(portfolios);
        }

        public ActionResult Portfolio(int year, int month, int day, string title)
        {
            var portfolio = this._postService.GetPortfolio(new DateTime(year, month, day), title.Replace('-', ' '));

            if (portfolio == null)
            {
                return this.RedirectToAction("notfound", "error");
            }

            return this.View(portfolio);
        }

        public ActionResult WritePortfolio(int id = 0)
        {
            if (id == 0)
            {
                return this.View(new Portfolio { Images = new Collection<Image>() });
            }

            var portfolio = this._postService.GetPortfolio(id);

            return this.View(portfolio);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult WritePortfolio(Portfolio portfolio, ICollection<string> image)
        {
            ICollection<Image> images = new Collection<Image>();

            foreach (var path in image.Where(i => !string.IsNullOrEmpty(i)))
            {
                images.Add(new Image { Path = path, Portfolio = portfolio });
            }

            var savedPortfolio = this._postService.SavePortfolio(portfolio);

            this._postService.SavePortfolioImages(savedPortfolio, images);

            return this.RedirectToAction("WritePortfolio", "Posts", new { id = savedPortfolio.Id });
        }

        public ActionResult WordCloud()
        {
            var tags = this._postService.GetAllTags().ToList();

            var cloud = tags.Distinct().Select(tag => new { text = tag, weight = tags.Count(t => t == tag) });

            return this.Json(cloud, JsonRequestBehavior.AllowGet);
        }
    }
}