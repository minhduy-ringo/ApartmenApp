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
    builder.EntitySet<Department>("Departments");
    builder.EntitySet<Staff>("Staffs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DepartmentsController : ODataController
    {
        private StaffContext db = new StaffContext();

        // GET: odata/Departments
        [EnableQuery]
        public IQueryable<Department> GetDepartments()
        {
            return db.Departments;
        }

        // GET: odata/Departments(5)
        [EnableQuery]
        public SingleResult<Department> GetDepartment([FromODataUri] short key)
        {
            return SingleResult.Create(db.Departments.Where(department => department.departmentId == key));
        }

        // PUT: odata/Departments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Department> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department department = await db.Departments.FindAsync(key);
            if (department == null)
            {
                return NotFound();
            }

            patch.Put(department);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(department);
        }

        // POST: odata/Departments
        public async Task<IHttpActionResult> Post(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            await db.SaveChangesAsync();

            return Created(department);
        }

        // PATCH: odata/Departments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Department> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department department = await db.Departments.FindAsync(key);
            if (department == null)
            {
                return NotFound();
            }

            patch.Patch(department);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(department);
        }

        // DELETE: odata/Departments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Department department = await db.Departments.FindAsync(key);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Departments(5)/Staffs
        [EnableQuery]
        public IQueryable<Staff> GetStaffs([FromODataUri] short key)
        {
            return db.Departments.Where(m => m.departmentId == key).SelectMany(m => m.Staffs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(short key)
        {
            return db.Departments.Count(e => e.departmentId == key) > 0;
        }
    }
}
