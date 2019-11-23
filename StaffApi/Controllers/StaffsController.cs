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
    builder.EntitySet<Staff>("Staffs");
    builder.EntitySet<Department>("Departments"); 
    builder.EntitySet<LeaveRequest>("LeaveRequests"); 
    builder.EntitySet<StaffVacation>("StaffVacations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StaffsController : ODataController
    {
        private StaffContext db = new StaffContext();

        // GET: odata/Staffs
        [EnableQuery]
        public IQueryable<Staff> GetStaffs()
        {
            return db.Staffs;
        }

        // GET: odata/Staffs(5)
        [EnableQuery]
        public SingleResult<Staff> GetStaff([FromODataUri] int key)
        {
            return SingleResult.Create(db.Staffs.Where(staff => staff.staffId == key));
        }

        // PUT: odata/Staffs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Staff> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Staff staff = await db.Staffs.FindAsync(key);
            if (staff == null)
            {
                return NotFound();
            }

            patch.Put(staff);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(staff);
        }

        // POST: odata/Staffs
        public async Task<IHttpActionResult> Post(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(staff);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StaffExists(staff.staffId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(staff);
        }

        // PATCH: odata/Staffs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Staff> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Staff staff = await db.Staffs.FindAsync(key);
            if (staff == null)
            {
                return NotFound();
            }

            patch.Patch(staff);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(staff);
        }

        // DELETE: odata/Staffs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Staff staff = await db.Staffs.FindAsync(key);
            if (staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(staff);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Staffs(5)/Departments
        [EnableQuery]
        public IQueryable<Department> GetDepartments([FromODataUri] int key)
        {
            return db.Staffs.Where(m => m.staffId == key).SelectMany(m => m.Departments);
        }

        // GET: odata/Staffs(5)/Department
        [EnableQuery]
        public SingleResult<Department> GetDepartment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Staffs.Where(m => m.staffId == key).Select(m => m.Department));
        }

        // GET: odata/Staffs(5)/LeaveRequests
        [EnableQuery]
        public IQueryable<LeaveRequest> GetLeaveRequests([FromODataUri] int key)
        {
            return db.Staffs.Where(m => m.staffId == key).SelectMany(m => m.LeaveRequests);
        }

        // GET: odata/Staffs(5)/StaffVacations
        [EnableQuery]
        public IQueryable<StaffVacation> GetStaffVacations([FromODataUri] int key)
        {
            return db.Staffs.Where(m => m.staffId == key).SelectMany(m => m.StaffVacations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int key)
        {
            return db.Staffs.Count(e => e.staffId == key) > 0;
        }
    }
}
