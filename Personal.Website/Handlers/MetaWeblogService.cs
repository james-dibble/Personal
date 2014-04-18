using System;
using System.Configuration;
using System.Linq;
using CookComputing.XmlRpc;
using Personal.DomainModel;
using Personal.Persistence;
using Personal.ServiceLayer;

public class MetaWeblogService : XmlRpcService, IMetaWeblog
{
    private readonly IPostService _postService;

    public MetaWeblogService()
    {
        this._postService = new PostService(new UnitOfWork(new PersonalPersistenceContext()));
    }

    public string AddPost(string blogid, string username, string password, Post post, bool publish)
    {
        Authenticate(password);

        var blog = new Blog
        {
            Title = post.Title,
            Date = post.PubDate,
            Tags = string.Join(",", post.Categories),
            Content = post.Content,
            Abstract = post.Slug
        };

        blog = this._postService.SaveBlog(blog);

        return blog.Id.ToString();
    }

    public bool UpdatePost(string postid, string username, string password, Post post, bool publish)
    {
        Authenticate(password);

        var blog = this._postService.GetBlog(int.Parse(postid));

        blog.Title = post.Title;
        blog.Date = post.LastModified == DateTime.MinValue ? post.PubDate : post.LastModified;
        blog.Tags = string.Join(",", post.Categories);
        blog.Content = post.Content;
        blog.Abstract = post.Slug;

        blog = this._postService.SaveBlog(blog);

        return true;
    }

    public object GetPost(string postid, string username, string password)
    {
        Authenticate(password);

        var blog = this._postService.GetBlog(int.Parse(postid));

        return new
        {
            description = blog.Content,
            title = blog.Title,
            dateCreated = blog.Date,
            wp_slug = blog.Abstract,
            categories = blog.Tags.Split(','),
            postid = blog.Id
        };
    }

    public object[] GetCategories(string blogid, string username, string password)
    {
        Authenticate(password);

        return this._postService.GetAllTags<Blog>().Select(t => new { title = t }).ToArray();
    }

    public object[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
    {
        Authenticate(password);

        var posts = this._postService.GetAllBlogs().OrderByDescending(b => b.Date).Take(numberOfPosts).Select(blog => new
        {
            description = blog.Content,
            title = blog.Title,
            dateCreated = blog.Date,
            wp_slug = blog.Abstract,
            categories = blog.Tags.Split(','),
            postid = blog.Id
        });

        return posts.ToArray();
    }

    public object NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
    {
        throw new System.NotImplementedException();
    }

    public bool DeletePost(string key, string postid, string username, string password, bool publish)
    {
        Authenticate(password);

        this._postService.DeleteBlog(int.Parse(postid));

        return true;
    }

    public object[] GetUsersBlogs(string key, string username, string password)
    {
        Authenticate(password);

        return new[] 
        { 
            new 
            {
                blogid = "1",
                blogName = "Personal",
                url = Context.Request.Url.Scheme + "://" + Context.Request.Url.Authority
            }
        };
    }

    private void Authenticate(string password)
    {
        var hashed = password.ComputeHash("iamasalt");
        var saved = ConfigurationManager.AppSettings["blogPublishPassword"];

        if (string.Equals(password.ComputeHash(), ConfigurationManager.AppSettings["blogPublishPassword"]))
        {
            throw new XmlRpcFaultException(0, "Password incorrect");
        }
    }
}