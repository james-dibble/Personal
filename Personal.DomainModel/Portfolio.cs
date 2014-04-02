namespace Personal.DomainModel
{
    using System.Collections.Generic;

    public class Portfolio : Post
    {
        public virtual ICollection<Image> Images { get; set; }
    }
}