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
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData;
using StaffApi.Models;

namespace StaffApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using StaffApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Notice>("Notices");
    builder.EntitySet<Staff>("Staffs"); 
    builder.EntitySet<NoticeReceiver>("NoticeReceivers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class NoticesController : ODataController
    {
        private StaffContext db = new StaffContext();

        [HttpGet]
        public IQueryable<Notice> GetStaffNotices(int userId)
        {
            IQueryable<NoticeReceiver> list = db.NoticeReceivers.Where(d => d.receiverId == userId);
            IQueryable<Notice> list2 = Enumerable.Empty<Notice>().AsQueryable();
            foreach (var a in list)
            {
                var temp = db.Notices.First(d => d.noticeId == a.noticeId);
                list2.ToList().Add(temp);
            }
            return list2.AsQueryable();
        }
        // GET: odata/Notices
        [EnableQuery]
        public IQueryable<Notice> GetNotices()
        {
            return db.Notices;
        }

        // GET: odata/Notices(5)
        [EnableQuery]
        public SingleResult<Notice> GetNotice([FromODataUri] int key)
        {
            return SingleResult.Create(db.Notices.Where(notice => notice.noticeId == key));
        }

        // PUT: odata/Notices(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Notice> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Notice notice = await db.Notices.FindAsync(key);
            if (notice == null)
            {
                return NotFound();
            }

            patch.Put(notice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(notice);
        }

        // POST: odata/Notices
        public async Task<IHttpActionResult> Post(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notices.Add(notice);
            await db.SaveChangesAsync();

            return Created(notice);
        }

        // PATCH: odata/Notices(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Notice> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Notice notice = await db.Notices.FindAsync(key);
            if (notice == null)
            {
                return NotFound();
            }

            patch.Patch(notice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(notice);
        }

        // DELETE: odata/Notices(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Notice notice = await db.Notices.FindAsync(key);
            if (notice == null)
            {
                return NotFound();
            }

            db.Notices.Remove(notice);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Notices(5)/Staff
        [EnableQuery]
        public SingleResult<Staff> GetStaff([FromODataUri] int key)
        {
            return SingleResult.Create(db.Notices.Where(m => m.noticeId == key).Select(m => m.Staff));
        }

        // GET: odata/Notices(5)/NoticeReceivers
        [EnableQuery]
        public IQueryable<NoticeReceiver> GetNoticeReceivers([FromODataUri] int key)
        {
            return db.Notices.Where(m => m.noticeId == key).SelectMany(m => m.NoticeReceivers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoticeExists(int key)
        {
            return db.Notices.Count(e => e.noticeId == key) > 0;
        }
    }
}
