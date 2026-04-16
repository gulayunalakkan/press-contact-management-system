namespace BasinTakip.EntityFramework.Migrations
{
    using BasinTakip.Domain.Entities.Base;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BasinTakip.EntityFramework.Context.CommonContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BasinTakip.EntityFramework.Context.CommonContext context)
        {
            var users = context.Set<User>();

            if (!users.Any(x => x.UserName == "admin"))
            {
                users.Add(new User()
                {
                    UserName = "admin",
                    PasswordHash = "1234"
                });
                context.SaveChanges();
                //  This method will be called after migrating to the latest version.

                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data. E.g.
                //
                //    context.People.AddOrUpdate(
                //      p => p.FullName,
                //      new Person { FullName = "Andrew Peters" },
                //      new Person { FullName = "Brice Lambson" },
                //      new Person { FullName = "Rowan Miller" }
                //    );
                //
            }
        }
    }
}
