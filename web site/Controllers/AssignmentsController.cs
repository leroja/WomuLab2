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
        public IQueryable<Assignment> GetAssignments()
        {
            return db.Assignments;
        }

        /// <summary>
        /// Looks up some Assignment by ID.
        /// </summary>
        /// <param name="id">The ID of the assignment.</param>
        // GET: api/Assignments/5
        [ResponseType(typeof(Assignment))]
        public IHttpActionResult GetAssignment(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }

            return Ok(assignment);
        }

        /// <summary>
        /// Update an existing assignment.
        /// </summary>
        /// <param name="id">ID Of the assignment</param>
        /// <param name="assignment">
        /// assigment info
        /// </param>
        // PUT: api/Assignments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssignment(int id, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assignment.TaskID)
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
                if (!AssignmentExists(id))
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
        /// 
        /// </param>
        // POST: api/Assignments
        [ResponseType(typeof(Assignment))]
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
                if (AssignmentExists(assignment.TaskID))
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
        /// <param name="id">
        /// ID of assignment
        /// </param>
        // DELETE: api/Assignments/5
        [ResponseType(typeof(Assignment))]
        public IHttpActionResult DeleteAssignment(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
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

        private bool AssignmentExists(int id)
        {
            return db.Assignments.Count(e => e.TaskID == id) > 0;
        }
    }
}