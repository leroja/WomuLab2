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
                            TaskTitle = a.Task.Title,
                            UserID = a.UserID,
                            UserForName = a.User.FirstName,
                            UserLastName = a.User.LastName

                        };

            return Assignments;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskID">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        // GET: api/Assignments/1
        [ResponseType(typeof(AssignmentDTO))]
        public IQueryable<AssignmentDTO> GetStatus(int TaskID)
        {


            var Assignments = from a in db.Assignments where a.TaskID == TaskID
                                select new AssignmentDTO()
                                {
                                    TaskID = a.TaskID,
                                    TaskTitle = a.Task.Title,
                                    UserID = a.UserID,
                                    UserForName = a.User.FirstName,
                                    UserLastName = a.User.LastName

                                };

            return Assignments;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        [ResponseType(typeof(AssignmentDTO))]
        public IQueryable<AssignmentDTO> GetUserAssignments(int UserID)
        {


            var Assignments = from a in db.Assignments
                              where a.UserID == UserID
                              select new AssignmentDTO()
                              {
                                  TaskID = a.TaskID,
                                  TaskTitle = a.Task.Title,
                                  UserID = a.UserID,
                                  UserForName = a.User.FirstName,
                                  UserLastName = a.User.LastName
                              };

            return Assignments;

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
        public IHttpActionResult GetAssignment(int taskID,int userID)
        {

            //Assignment assignment = db.Assignments.Find(taskID, userID);


            var Ass = db.Assignments.Find(taskID, userID);

            AssignmentDTO assignmentDTO = new AssignmentDTO
            {
                TaskID = Ass.TaskID,
                TaskTitle = Ass.Task.Title,
                UserID = Ass.UserID,
                UserForName = Ass.User.FirstName,
                UserLastName = Ass.User.LastName
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
        /// <param name="assignment">
        /// assignment info
        /// </param>
        // POST: api/Assignments
        [ResponseType(typeof(AssignmentDTO))]
        public IHttpActionResult PostAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assignments.Add(assignment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AssignmentExists(assignment.TaskID,assignment.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = assignment.TaskID }, assignment);
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
    }
}