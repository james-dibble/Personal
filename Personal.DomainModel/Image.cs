namespace Personal.DomainModel
{
    public class Image : IDomainObject
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}