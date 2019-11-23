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
    builder.EntitySet<StaffVacation>("StaffVacations");
    builder.EntitySet<Staff>("Staffs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StaffVacationsController : ODataController
    {
        private StaffContext db = new StaffContext();

        // GET: odata/StaffVacations
        [EnableQuery]
        public IQueryable<StaffVacation> GetStaffVacations()
        {
            return db.StaffVacations;
        }

        // GET: odata/StaffVacations(5)
        [EnableQuery]
        public SingleResult<StaffVacation> GetStaffVacation([FromODataUri] int key)
        {
            return SingleResult.Create(db.StaffVacations.Where(staffVacation => staffVacation.staffVacationId == key));
        }

        // PUT: odata/StaffVacations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<StaffVacation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StaffVacation staffVacation = await db.StaffVacations.FindAsync(key);
            if (staffVacation == null)
            {
                return NotFound();
            }

            patch.Put(staffVacation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffVacationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(staffVacation);
        }

        // POST: odata/StaffVacations
        public async Task<IHttpActionResult> Post(StaffVacation staffVacation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StaffVacations.Add(staffVacation);
            await db.SaveChangesAsync();

            return Created(staffVacation);
        }

        // PATCH: odata/StaffVacations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<StaffVacation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StaffVacation staffVacation = await db.StaffVacations.FindAsync(key);
            if (staffVacation == null)
            {
                return NotFound();
            }

            patch.Patch(staffVacation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffVacationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(staffVacation);
        }

        // DELETE: odata/StaffVacations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            StaffVacation staffVacation = await db.StaffVacations.FindAsync(key);
            if (staffVacation == null)
            {
                return NotFound();
            }

            db.StaffVacations.Remove(staffVacation);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StaffVacations(5)/Staff
        [EnableQuery]
        public SingleResult<Staff> GetStaff([FromODataUri] int key)
        {
            return SingleResult.Create(db.StaffVacations.Where(m => m.staffVacationId == key).Select(m => m.Staff));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffVacationExists(int key)
        {
            return db.StaffVacations.Count(e => e.staffVacationId == key) > 0;
        }
    }
}
