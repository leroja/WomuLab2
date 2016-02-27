using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using web_site.Models;
using web_site.ViewModels;

namespace web_site.Controllers
{
    public class AssignmentsController : ApiController
    {
        private ProjectDatabase db = new ProjectDatabase();

        /// <summary>
        /// Gets the assigments from the server.
        /// </summary>
        // GET: api/Assignments
        public IQueryable<AssignmentDTO> GetAssignments()
        {

            var Assignments = from a in db.Assignments
                              select new AssignmentDTO()
                              {
                                  TaskID = a.TaskID,
                                  Title = a.Task.Title,
                                  BeginDateTime = a.Task.BeginDateTime,
                                  DeadlineDateTime = a.Task.DeadlineDateTime,
                                  Requirements = a.Task.Requirements
                              };
            foreach (var item in Assignments)
            {
                item.Users = test(item.TaskID);
            }

            return Assignments;

        }

        /// <summary>
        /// finds and return the names of all users assigned to a task 
        /// </summary>
        /// <param name="TaskID">
        /// Id of task
        /// </param>
        // GET: api/Assignments/1
        [ResponseType(typeof(string))]
        public IQueryable<string> GetStatus(int TaskID)
        {

            List<string> t = test(TaskID);


            return t.AsQueryable();
        }

        /// <summary>
        /// gets all assignments that belong to a specific user
        /// </summary>
        /// <param name="UserID">
        /// Id of user
        /// </param>
        /// // GET: api/Assignments/1
        [ResponseType(typeof(AssignmentDTO))]
        public IQueryable<AssignmentDTO> GetUserAssignments(int UserID)
        {
            
            var Assignments = from a in db.Assignments
                              where a.UserID == UserID
                              select new AssignmentDTO()
                              {
                                  TaskID = a.TaskID,
                                  Title = a.Task.Title,
                                  BeginDateTime = a.Task.BeginDateTime,
                                  DeadlineDateTime = a.Task.DeadlineDateTime,
                                  Requirements = a.Task.Requirements
                            };

            List<AssignmentDTO> tet = Assignments.ToList();

            foreach (var item in tet)
            {
                item.Users = test(item.TaskID);
            }

            return tet.AsQueryable<AssignmentDTO>();
            //return Assignments;
        }



        /// <summary>
        /// Looks up some Assignment by TaskID and UserID.
        /// </summary>
        /// <param name="taskID">
        /// ID of the associated task
        /// </param>
        /// <param name="userID">
        /// ID of the associated User
        /// </param>
        /// <returns></returns>
        // GET: api/Assignments/5
        [ResponseType(typeof(AssignmentDTO))]
        public IHttpActionResult GetAssignment(int taskID, int userID)
        {

            var Ass = db.Assignments.Find(taskID, userID);

            AssignmentDTO assignmentDTO = new AssignmentDTO
            {
                TaskID = Ass.TaskID,
                Title = Ass.Task.Title,
                BeginDateTime = Ass.Task.BeginDateTime,
                DeadlineDateTime = Ass.Task.DeadlineDateTime,
                Requirements = Ass.Task.Requirements,
                Users = test(Ass.TaskID)
            };



            if (assignmentDTO == null)
            {
                return NotFound();
            }

            return Ok(assignmentDTO);
        }

        /// <summary>
        /// Update an existing assignment.
        /// </summary>
        /// <param name="taskID">
        /// ID of the associated task
        /// </param>
        /// <param name="userID">
        /// ID of the associated User
        /// </param>
        /// <param name="assignment">
        /// assigment info
        /// </param>
        // PUT: api/Assignments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssignment(int taskID, int userID, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (taskID != assignment.TaskID || userID != assignment.UserID)
            {
                return BadRequest();
            }

            db.Entry(assignment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(taskID, userID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new assignment
        /// </summary>
        /// <param >
        /// assignment info
        /// </param>
        // POST: api/Assignments
        [ResponseType(typeof(String))]
        public IHttpActionResult PostAssignment(int UserID, int TaskID){

            User use = db.Users.Find(UserID);
            Task task = db.Tasks.Find(TaskID);

            Assignment ass = db.Assignments.Find(TaskID,UserID);
            if (ass != null){
                return NotFound();
            }
            else
            {
                Assignment temp = new Assignment
                {
                    UserID = use.UserID,
                    TaskID = task.TaskID,
                    User = use,
                    Task = task
                };
                db.Assignments.Add(temp);
                db.SaveChanges();
                return Ok("added in db");
            }
        }

        /// <summary>
        /// Delete an assignment
        /// </summary>
        /// <param name="taskID">
        /// ID of the associated task
        /// </param>
        /// <param name="userID">
        /// ID of the associated User
        /// </param>
        // DELETE: api/Assignments/5
        [ResponseType(typeof(Assignment))]
        public IHttpActionResult DeleteAssignment(int taskID,int userID)
        {
            Assignment assignment = db.Assignments.Find(taskID, userID);
            if (assignment == null)
            {
                return NotFound();
            }

            db.Assignments.Remove(assignment);
            db.SaveChanges();

            return Ok(assignment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignmentExists(int taskID, int userID)
        {
            return db.Assignments.Count(e => e.TaskID == taskID && e.UserID == userID) > 0;
        }


        /// <summary>
        /// finds all assignments to a task and returns a 
        /// list of usernames of users who are assigned to the task
        /// </summary>
        /// <param name="Id">
        /// Id of the task
        /// </param>
        /// <returns>
        /// a list of names or a list that contains a single string ("none")
        /// if there are no users assigned to the task
        /// </returns>
        private List<string> test(int Id)
        {
            var t = db.Assignments.Where(a => a.TaskID == Id);
            var s = t.ToList();
            List<string> users = new List<string>();

            if (s.Count() == 0)
            {
                List<string> a = new List<string>();
                a.Add("None");
                return a;
            }

            foreach (var item in s)
            {
                string first = item.User.FirstName;
                string last = item.User.LastName;

                string name = first + " " + last;
                users.Add(name);
            }

            return users;
        }
    }
}