using System.Data.Entity;
using Personal.DomainModel;

namespace Personal.Persistence
{
    using Personal.Persistence.Migrations;

    public class PersonalPersistenceContext : DbContext
    {
        public PersonalPersistenceContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>()
                .HasMany(p => p.Images);
        }

        public IDbSet<Blog> Blogs { get; set; }

        public IDbSet<Portfolio> Portfolios { get; set; }

        public IDbSet<Image> Images { get; set; }
    }
}