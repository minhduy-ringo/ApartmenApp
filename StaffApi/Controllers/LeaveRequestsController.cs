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
    builder.EntitySet<LeaveRequest>("LeaveRequests");
    builder.EntitySet<Staff>("Staffs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LeaveRequestsController : ODataController
    {
        private StaffContext db = new StaffContext();

        // GET: odata/LeaveRequests
        [EnableQuery]
        public IQueryable<LeaveRequest> GetLeaveRequests()
        {
            return db.LeaveRequests;
        }

        // GET: odata/LeaveRequests(5)
        [EnableQuery]
        public SingleResult<LeaveRequest> GetLeaveRequest([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveRequests.Where(leaveRequest => leaveRequest.leaveRequestId == key));
        }

        // PUT: odata/LeaveRequests(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LeaveRequest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeaveRequest leaveRequest = await db.LeaveRequests.FindAsync(key);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            patch.Put(leaveRequest);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leaveRequest);
        }

        // POST: odata/LeaveRequests
        public async Task<IHttpActionResult> Post(LeaveRequest leaveRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeaveRequests.Add(leaveRequest);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LeaveRequestExists(leaveRequest.leaveRequestId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(leaveRequest);
        }

        // PATCH: odata/LeaveRequests(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LeaveRequest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeaveRequest leaveRequest = await db.LeaveRequests.FindAsync(key);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            patch.Patch(leaveRequest);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leaveRequest);
        }

        // DELETE: odata/LeaveRequests(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            LeaveRequest leaveRequest = await db.LeaveRequests.FindAsync(key);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            db.LeaveRequests.Remove(leaveRequest);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LeaveRequests(5)/Staff
        [EnableQuery]
        public SingleResult<Staff> GetStaff([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveRequests.Where(m => m.leaveRequestId == key).Select(m => m.Staff));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaveRequestExists(int key)
        {
            return db.LeaveRequests.Count(e => e.leaveRequestId == key) > 0;
        }
    }
}
