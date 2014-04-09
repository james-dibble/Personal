namespace Personal.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Personal.DomainModel;
    using Personal.Persistence;

    public class PostService : IPostService
    {
        private readonly IUnitOfWork _persistence;

        public PostService(IUnitOfWork persistence)
        {
            this._persistence = persistence;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var blogs = this._persistence.GetRepository<Blog>().GetAll();

            var portfolios = this._persistence.GetRepository<Portfolio>().GetAll();

            var posts = new List<Post>();

            posts.AddRange(blogs);
            posts.AddRange(portfolios);

            return posts.OrderByDescending(p => p.Date.Date);
        }

        public Blog GetBlog(DateTime date, string title)
        {
            if (this._persistence.GetRepository<Blog>().Count(b => b.Date.Year == date.Year && b.Date.Month == date.Month && b.Date.Day == date.Day) == 1)
            {
                return this._persistence.GetRepository<Blog>().Single(b => b.Date.Year == date.Year && b.Date.Month == date.Month && b.Date.Day == date.Day);
            }

            var blog =
                this._persistence.GetRepository<Blog>()
                    .First(b => b.Date.Year == date.Year && b.Date.Month == date.Month && b.Date.Day == date.Day && b.Title.ToLower() == title.ToLower());

            return blog;
        }

        public Blog GetBlog(int id)
        {
            var blog = this._persistence.GetRepository<Blog>().Single(b => b.Id == id);

            return blog;
        }

        public Blog GetBlog(string title)
        {
            var blog = this._persistence.GetRepository<Blog>().Single(b => b.Title.ToLower() == title.ToLower());

            return blog;
        }

        public Blog SaveBlog(Blog blog)
        {
            if (blog.Id == 0)
            {
                this._persistence.GetRepository<Blog>().Add(blog);
            }
            else
            {
                this._persistence.GetRepository<Blog>().Update(blog);   
            }

            this._persistence.Commit();

            return blog;
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            var blogs = this._persistence.GetRepository<Blog>().GetAll();

            return blogs;
        }

        public IEnumerable<Blog> GetBlogs(int year = 0, int month = 0)
        {
            if (year == 0 && month == 0)
            {
                return this.GetAllBlogs();
            }

            if (month == 0)
            {
                return this._persistence.GetRepository<Blog>().GetMany(b => b.Date.Year == year);
            }

            if (year == 0)
            {
                return this._persistence.GetRepository<Blog>().GetMany(b => b.Date.Month == month);
            }

            return this._persistence.GetRepository<Blog>().GetMany(b => b.Date.Year == year && b.Date.Month == month);
        }

        public Portfolio GetPortfolio(DateTime date, string title)
        {
            if (this._persistence.GetRepository<Portfolio>().Count(b => b.Date.Year == date.Year && b.Date.Month == date.Month && b.Date.Day == date.Day) == 1)
            {
                return this._persistence.GetRepository<Portfolio>().Single(b => b.Date.Year == date.Year && b.Date.Month == date.Month && b.Date.Day == date.Day);
            }

            var portfolio =
                this._persistence.GetRepository<Portfolio>()
                    .First(b => b.Date.Year == date.Year && b.Date.Month == date.Month && b.Date.Day == date.Day && b.Title.ToLower() == title.ToLower());

            return portfolio;
        }

        public Portfolio GetPortfolio(int id)
        {
            var portfolio = this._persistence.GetRepository<Portfolio>().Single(b => b.Id == id);

            return portfolio;
        }

        public Portfolio GetPortfolio(string title)
        {
            var portfolio = this._persistence.GetRepository<Portfolio>().Single(b => b.Title.ToLower() == title.ToLower());

            return portfolio;
        }

        public Portfolio SavePortfolio(Portfolio portfolio)
        {
            if (portfolio.Id == 0)
            {
                this._persistence.GetRepository<Portfolio>().Add(portfolio);
            }
            else
            {
                this._persistence.GetRepository<Portfolio>().Update(portfolio);
            }

            this._persistence.Commit();

            return portfolio;
        }

        public void SavePortfolioImages(Portfolio portfolio, ICollection<Image> images)
        {
            this._persistence.GetRepository<Image>().Delete(i => i.Portfolio.Id == portfolio.Id);

            this._persistence.Commit();

            foreach (var image in images)
            {
                this._persistence.GetRepository<Image>().Add(image);
            }

            this._persistence.Commit();
        }

        public IEnumerable<Portfolio> GetAllPortfolios()
        {
            var portfolios = this._persistence.GetRepository<Portfolio>().GetAll();

            return portfolios;
        }

        public IEnumerable<string> GetAllTags()
        {
            var blogsWithTags = this._persistence.GetRepository<Blog>().GetMany(b => b.Tags != null);

            var tags = blogsWithTags.SelectMany(b => b.Tags.Split(','));

            return tags;
        }
    }
}