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
using StaffApi.Models;

namespace StaffApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using StaffApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Schedule>("Schedules");
    builder.EntitySet<Staff>("Staffs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SchedulesController : ODataController
    {
        private StaffContext db = new StaffContext();

        // GET: odata/Schedules
        [EnableQuery]
        public IQueryable<Schedule> GetSchedules()
        {
            return db.Schedules;
        }

        // GET: odata/Schedules(5)
        [EnableQuery]
        public SingleResult<Schedule> GetSchedule([FromODataUri] int key)
        {
            return SingleResult.Create(db.Schedules.Where(schedule => schedule.scheduleId == key));
        }

        // PUT: odata/Schedules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Schedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Schedule schedule = await db.Schedules.FindAsync(key);
            if (schedule == null)
            {
                return NotFound();
            }

            patch.Put(schedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schedule);
        }

        // POST: odata/Schedules
        public async Task<IHttpActionResult> Post(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schedules.Add(schedule);
            await db.SaveChangesAsync();

            return Created(schedule);
        }

        // PATCH: odata/Schedules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Schedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Schedule schedule = await db.Schedules.FindAsync(key);
            if (schedule == null)
            {
                return NotFound();
            }

            patch.Patch(schedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schedule);
        }

        // DELETE: odata/Schedules(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Schedule schedule = await db.Schedules.FindAsync(key);
            if (schedule == null)
            {
                return NotFound();
            }

            db.Schedules.Remove(schedule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Schedules(5)/Staff
        [EnableQuery]
        public SingleResult<Staff> GetStaff([FromODataUri] int key)
        {
            return SingleResult.Create(db.Schedules.Where(m => m.scheduleId == key).Select(m => m.Staff));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleExists(int key)
        {
            return db.Schedules.Count(e => e.scheduleId == key) > 0;
        }
    }
}
