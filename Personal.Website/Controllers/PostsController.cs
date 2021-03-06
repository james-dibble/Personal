﻿using System.ServiceModel.Syndication;
using JamesDibble.ApplicationFramework.Web.Rss;

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

        [OutputCache(Duration = 3600)]
        public ActionResult BlogArchive(int year, int month, int page)
        {
            var blogs = this._postService.GetBlogs(year, month).OrderByDescending(b => b.Date);

            var viewModel = new BlogHeaderViewModel { Year = year, Month = month, Blogs = blogs, Page = page };

            return this.View(viewModel);
        }

        public ActionResult BlogRssFeed()
        {
            var feed = BuildFeed();

            return this.RssResult(feed);
        }

        public ActionResult BlogAtomFeed()
        {
            var feed = BuildFeed();

            return this.AtomResult(feed);
        }

        private SyndicationFeed BuildFeed()
        {
            var items = this._postService.GetAllBlogs().OrderByDescending(blog => blog.Date).Select(blog => new SyndicationItem
            {
                Title = SyndicationContent.CreatePlaintextContent(blog.Title),
                Summary = SyndicationContent.CreateHtmlContent(blog.Abstract),
                PublishDate = blog.Date
            }).ToList();

            foreach (var syndicationItem in items)
            {
                syndicationItem.AddPermalink(
                    new Uri(
                        new Uri("http://www.jdibble.co.uk"),
                        this.Url.Action(
                            "blog",
                            "posts",
                            new
                            {
                                year = syndicationItem.PublishDate.Year,
                                month = syndicationItem.PublishDate.Month,
                                day = syndicationItem.PublishDate.Day,
                                title = syndicationItem.Title.Text.ToLowerInvariant().Replace(' ', '-')
                            })));
            }

            foreach (var syndicationItem in items)
            {
                syndicationItem.Authors.Add(new SyndicationPerson("info@jdibble.co.uk", "James Dibble",
                    "http://www.jdibble.co.uk"));
            }

            var feed = new SyndicationFeed(
                "James Dibble - Blog",
                "Ramblings of a .Net Web Developer; the blog of James Dibble.",
                new Uri("http://www.jdibble.co.uk/blog/rss"),
                items)
            {
                Language = "en-gb",
                LastUpdatedTime = DateTime.Now,
                ImageUrl = new Uri("http://asset.jdibble.co.uk/image/favicon.ico")
            };

            return feed;
        }

        public ActionResult BlogArchiveTag(int year, int month, string tag, int page)
        {
            IEnumerable<Blog> blogs = null;

            if(string.IsNullOrEmpty(tag))
            {
                blogs = this._postService.GetBlogs(year, month).OrderByDescending(b => b.Date);
            }
            else
            {
                blogs = this._postService.GetBlogs(tag).OrderByDescending(p => p.Date);
            }

            var viewModel = new BlogHeaderViewModel { Year = year, Month = month, Blogs = blogs, Page = page, Tag = tag };

            return this.View("BlogArchive", viewModel);
        }

        public ActionResult BlogHeader(int year = 0, int month = 0, bool canRemove = true)
        {
            var blogs = this._postService.GetAllBlogs();

            var viewModel = new BlogHeaderViewModel { Year = year, Month = month, Blogs = blogs, CanRemove = canRemove };

            return this.PartialView(viewModel);
        }

        [OutputCache(Duration = 604800)]
        public ActionResult Blog(int year, int month, int day, string title)
        {
            var blog = this._postService.GetBlog(new DateTime(year, month, day), title.Replace('-', ' '));

            if (blog == null)
            {
                return this.RedirectToAction("notfound", "error");
            }

            var viewModel = new BlogViewModel
            {
                Blog = blog,
                NextBlog = this._postService.GetNextBlog(blog),
                PreviousBlog = this._postService.GetPreviousBlog(blog)
            };

            return this.View(viewModel);
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

        [OutputCache(Duration = 3600)]
        public ActionResult PortfolioArchive()
        {
            var portfolios = this._postService.GetAllPortfolios().OrderByDescending(p => p.Date);

            return this.View(portfolios);
        }

        [OutputCache(Duration = 3600)]
        public ActionResult PortfolioArchiveTag(string tag)
        {
            var portfolios = this._postService.GetPortfolios(tag).OrderByDescending(p => p.Date);

            return this.View("PortfolioArchive", portfolios);
        }

        [OutputCache(Duration = 604800)]
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

        public ActionResult Tags(Type postType)
        {
            IEnumerable<string> tags = null;

            if (postType == typeof(Portfolio))
            {
                tags = this._postService.GetAllTags<Portfolio>().ToList();
            }

            if (postType == typeof(Blog))
            {
                tags = this._postService.GetAllTags<Blog>().ToList();
            }

            var cloud =
                tags.Distinct().Select(tag => new KeyValuePair<string, int>(tag.Trim(), tags.Count(t => t == tag)));
            
            return this.PartialView(new TagViewModel { Tags = cloud, PostType = postType });
        }
    }
}