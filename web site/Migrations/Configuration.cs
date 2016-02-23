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
                new User() { UserID = 1, FirstName = "Lennart", LastName = "Jakobsson"},
                new User() { UserID = 2, FirstName = "Test", LastName = "Testsson" },
                new User() { UserID = 3, FirstName = "Alexander", LastName = "Pihl" }
            );

            context.Tasks.AddOrUpdate(x => x.TaskID, 
                new Task() { TaskID = 1, BeginDateTime = DateTime.Now, DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing"},
                new Task() { TaskID = 2, BeginDateTime = DateTime.Now.AddDays(1), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing2" },
                new Task() { TaskID = 3, BeginDateTime = DateTime.Now.AddHours(5).AddMinutes(3), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing3" },
                new Task() { TaskID = 4, BeginDateTime = DateTime.Now.AddMinutes(30), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing4" },
                new Task() { TaskID = 5, BeginDateTime = DateTime.Now.AddHours(1).AddDays(1), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing5" },
                new Task() { TaskID = 6, BeginDateTime = DateTime.Now.AddDays(2), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing6" },
                new Task() { TaskID = 7, BeginDateTime = DateTime.Now.AddMinutes(45), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing7" },
                new Task() { TaskID = 8, BeginDateTime = DateTime.Now.AddHours(12), DeadlineDateTime = DateTime.Now.AddDays(14),
                    Requirements = "I don't know", Title = "SomeThing8" }
            );

            var Assignments = new List<Assignment>
            {
               new Assignment {TaskID = 1, UserID = 1},
               new Assignment {TaskID = 2, UserID = 2},
               new Assignment {TaskID = 3, UserID = 1},
               new Assignment {TaskID = 4, UserID = 3},
               new Assignment {TaskID = 5, UserID = 2},
               new Assignment {TaskID = 6, UserID = 1},
               new Assignment {TaskID = 7, UserID = 3}
            };
            foreach (Assignment e in Assignments)
            {
                var enrollmentInDataBase = context.Assignments.Where(
                    s => s.UserID == e.UserID && s.TaskID == e.TaskID);
                if (enrollmentInDataBase == null)
                {
                    context.Assignments.Add(e);
                }
            }
        }
    }
}
