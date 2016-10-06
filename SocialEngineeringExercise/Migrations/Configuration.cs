namespace SocialEngineeringExercise.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SocialEngineeringExercise.Models.SocialEnginnringModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SocialEngineeringExercise.Models.SocialEnginnringModel context)
        {
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

            context.SocialEnginnringReply.AddOrUpdate(
                p => p.SocialEnginnringGuid,
                new SocialEnginnringReplyModel()
                {
                    SocialEnginnringGuid = Guid.NewGuid(),
                    EmployeeNo = "0001",
                    EmployeeEmail = "justin@hotmail.com",
                    EmployeeName = "Test",
                    ClickTime = 0,
                    Remark = ""
                }, new SocialEnginnringReplyModel()
                {
                    SocialEnginnringGuid = Guid.NewGuid(),
                    EmployeeNo = "0002",
                    EmployeeEmail = "justin@hotmail.com",
                    EmployeeName = "Test",
                    ClickTime = 0,
                    Remark = ""
                }
                );
        }
    }
}
