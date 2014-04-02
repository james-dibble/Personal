using System.Collections.ObjectModel;
using Personal.DomainModel;

namespace Personal.Persistence.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Personal.Persistence.PersonalPersistenceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Personal.Persistence.PersonalPersistenceContext context)
        {
        }
    }
}
