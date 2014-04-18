namespace Personal.Website.ViewModels
{
    using Personal.DomainModel;

    public class BlogViewModel
    {
        public Blog Blog { get; set; }

        public Blog NextBlog { get; set; }

        public Blog PreviousBlog { get; set; }
    }
}