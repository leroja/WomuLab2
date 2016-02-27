namespace web_site.Migrations
{
    using web_site.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<web_site.Models.ProjectDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(web_site.Models.ProjectDatabase context)
        {

            context.Users.AddOrUpdate(x => x.UserID,
                new User() { UserID = 1, FirstName = "Lennart",     LastName = "Jakobsson"},
                new User() { UserID = 2, FirstName = "Test",        LastName = "Testsson" },
                new User() { UserID = 3, FirstName = "Alexander",   LastName = "Pihl" },
                new User() { UserID = 4, FirstName = "Thora",       LastName = "Rot" },
                new User() { UserID = 5, FirstName = "Kieron",      LastName = "Simon" },
                new User() { UserID = 6, FirstName = "Fedya",       LastName = "Coeman" },
                new User() { UserID = 7, FirstName = "Israa",       LastName = "Pinho" }
            );

            context.Tasks.AddOrUpdate(x => x.TaskID, 
                new Task() { TaskID = 1, BeginDateTime = DateTime.Now, DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing"},
                new Task() { TaskID = 2, BeginDateTime = DateTime.Now.AddDays(1), DeadlineDateTime = DateTime.Now.AddDays(5),
                    Requirements = "I don't know", Title = "SomeThing2" },
                new Task() { TaskID = 3, BeginDateTime = DateTime.Now.AddHours(5).AddMinutes(3), DeadlineDateTime = DateTime.Now.AddDays(8).AddHours(12),
                    Requirements = "I don't know", Title = "SomeThing3" },
                new Task() { TaskID = 4, BeginDateTime = DateTime.Now.AddMinutes(30), DeadlineDateTime = DateTime.Now.AddDays(3).AddMinutes(135),
                    Requirements = "I don't know", Title = "SomeThing4" },
                new Task() { TaskID = 5, BeginDateTime = DateTime.Now.AddHours(1).AddDays(1), DeadlineDateTime = DateTime.Now.AddDays(2).AddHours(12),
                    Requirements = "I don't know", Title = "SomeThing5" },
                new Task() { TaskID = 6, BeginDateTime = DateTime.Now.AddDays(2), DeadlineDateTime = DateTime.Now.AddDays(3),
                    Requirements = "I don't know", Title = "SomeThing6" },
                new Task() { TaskID = 7, BeginDateTime = DateTime.Now.AddMinutes(45), DeadlineDateTime = DateTime.Now.AddHours(12),
                    Requirements = "I don't know", Title = "SomeThing7" },
                new Task() { TaskID = 8, BeginDateTime = DateTime.Now.AddHours(12), DeadlineDateTime = DateTime.Now.AddDays(1),
                    Requirements = "I don't know", Title = "SomeThing8" }
            );


            context.Assignments.AddOrUpdate(p => new { p.TaskID,p.UserID }, new Assignment { TaskID = 1, UserID = 1 },
               new Assignment { TaskID = 2, UserID = 2 },
               new Assignment { TaskID = 3, UserID = 1 },
               new Assignment { TaskID = 4, UserID = 3 },
               new Assignment { TaskID = 5, UserID = 2 },
               new Assignment { TaskID = 6, UserID = 1 },
               new Assignment { TaskID = 7, UserID = 3 }
               );
        }
    }
}
