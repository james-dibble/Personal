namespace Personal.DomainModel
{
    using System;

    public abstract class Post : IDomainObject
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Abstract { get; set; }

        public string Content { get; set; }
    }
}