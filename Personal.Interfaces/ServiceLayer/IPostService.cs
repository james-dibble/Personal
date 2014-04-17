using System;

namespace Personal.ServiceLayer
{
    using System.Collections.Generic;
    using Personal.DomainModel;

    public interface IPostService
    {
        IEnumerable<Post> GetAllPosts();

        Blog GetBlog(DateTime date, string title);

        Blog GetBlog(int id);

        Blog GetBlog(string title);

        Blog SaveBlog(Blog blog);

        IEnumerable<Blog> GetAllBlogs();

        IEnumerable<Blog> GetBlogs(int year = 0, int month = 0);

        IEnumerable<Blog> GetBlogs(string tag);
        
        Portfolio GetPortfolio(DateTime date, string title);

        Portfolio GetPortfolio(int id);

        Portfolio GetPortfolio(string title);

        Portfolio SavePortfolio(Portfolio portfolio);

        void DeleteBlog(int id);

        void SavePortfolioImages(Portfolio portfolio, ICollection<Image> images);

        IEnumerable<Portfolio> GetAllPortfolios();

        IEnumerable<Portfolio> GetPortfolios(string tag);

        IEnumerable<string> GetAllTags<TPost>() where TPost : Post;
    }
}