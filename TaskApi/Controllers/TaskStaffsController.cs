using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using TaskApi.Models;
using Task = TaskApi.Models.Task;

namespace TaskApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TaskApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<TaskStaff>("TaskStaffs");
    builder.EntitySet<Task>("Tasks"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TaskStaffsController : ODataController
    {
        private TaskContext db = new TaskContext();

        // GET: odata/TaskStaffs
        [EnableQuery]
        public IQueryable<TaskStaff> GetTaskStaffs()
        {
            return db.TaskStaffs;
        }

        // GET: odata/TaskStaffs(5)
        [EnableQuery]
        public SingleResult<TaskStaff> GetTaskStaff([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskStaffs.Where(taskStaff => taskStaff.taskId == key));
        }

        // PUT: odata/TaskStaffs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TaskStaff> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskStaff taskStaff = await db.TaskStaffs.FindAsync(key);
            if (taskStaff == null)
            {
                return NotFound();
            }

            patch.Put(taskStaff);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskStaffExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(taskStaff);
        }

        // POST: odata/TaskStaffs
        public async Task<IHttpActionResult> Post(TaskStaff taskStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskStaffs.Add(taskStaff);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskStaffExists(taskStaff.taskId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(taskStaff);
        }

        // PATCH: odata/TaskStaffs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TaskStaff> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskStaff taskStaff = await db.TaskStaffs.FindAsync(key);
            if (taskStaff == null)
            {
                return NotFound();
            }

            patch.Patch(taskStaff);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskStaffExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(taskStaff);
        }

        // DELETE: odata/TaskStaffs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TaskStaff taskStaff = await db.TaskStaffs.FindAsync(key);
            if (taskStaff == null)
            {
                return NotFound();
            }

            db.TaskStaffs.Remove(taskStaff);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TaskStaffs(5)/Task
        [EnableQuery]
        public SingleResult<Task> GetTask([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskStaffs.Where(m => m.taskId == key).Select(m => m.Task));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskStaffExists(int key)
        {
            return db.TaskStaffs.Count(e => e.taskId == key) > 0;
        }
    }
}
