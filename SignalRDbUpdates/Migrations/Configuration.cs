using Microsoft.AspNet.Identity.EntityFramework;

namespace SignalRDbUpdates.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SignalRDbUpdates.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SignalRDbUpdates.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Roles.AddOrUpdate( new IdentityRole { Id = "1",Name = "Admin" } );
            context.SaveChanges();
        }
    }
}
