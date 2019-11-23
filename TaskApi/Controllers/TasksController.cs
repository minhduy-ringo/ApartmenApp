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
    builder.EntitySet<Task>("Tasks");
    builder.EntitySet<TaskType>("TaskTypes"); 
    builder.EntitySet<TaskStaff>("TaskStaffs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TasksController : ODataController
    {
        private TaskContext db = new TaskContext();

        // GET: odata/Tasks
        [EnableQuery]
        public IQueryable<Task> GetTasks()
        {
            return db.Tasks;
        }

        // GET: odata/Tasks(5)
        [EnableQuery]
        public SingleResult<Task> GetTask([FromODataUri] int key)
        {
            return SingleResult.Create(db.Tasks.Where(task => task.taskId == key));
        }

        // PUT: odata/Tasks(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Task> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Task task = await db.Tasks.FindAsync(key);
            if (task == null)
            {
                return NotFound();
            }

            patch.Put(task);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(task);
        }

        // POST: odata/Tasks
        public async Task<IHttpActionResult> Post(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return Created(task);
        }

        // PATCH: odata/Tasks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Task> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Task task = await db.Tasks.FindAsync(key);
            if (task == null)
            {
                return NotFound();
            }

            patch.Patch(task);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(task);
        }

        // DELETE: odata/Tasks(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Task task = await db.Tasks.FindAsync(key);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Tasks(5)/TaskType
        [EnableQuery]
        public SingleResult<TaskType> GetTaskType([FromODataUri] int key)
        {
            return SingleResult.Create(db.Tasks.Where(m => m.taskId == key).Select(m => m.TaskType));
        }

        // GET: odata/Tasks(5)/TaskStaffs
        [EnableQuery]
        public IQueryable<TaskStaff> GetTaskStaffs([FromODataUri] int key)
        {
            return db.Tasks.Where(m => m.taskId == key).SelectMany(m => m.TaskStaffs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int key)
        {
            return db.Tasks.Count(e => e.taskId == key) > 0;
        }
    }
}
