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
using ComplexApi.Models;

namespace ComplexApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ComplexApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Complex>("Complexes");
    builder.EntitySet<Building>("Buildings"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ComplexesController : ODataController
    {
        private ComplexContext db = new ComplexContext();

        // GET: odata/Complexes
        [EnableQuery]
        public IQueryable<Complex> GetComplexes()
        {
            return db.Complexes;
        }

        // GET: odata/Complexes(5)
        [EnableQuery]
        public SingleResult<Complex> GetComplex([FromODataUri] short key)
        {
            return SingleResult.Create(db.Complexes.Where(complex => complex.complexId == key));
        }

        // PUT: odata/Complexes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Complex> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Complex complex = await db.Complexes.FindAsync(key);
            if (complex == null)
            {
                return NotFound();
            }

            patch.Put(complex);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplexExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(complex);
        }

        // POST: odata/Complexes
        public async Task<IHttpActionResult> Post(Complex complex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complexes.Add(complex);
            await db.SaveChangesAsync();

            return Created(complex);
        }

        // PATCH: odata/Complexes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Complex> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Complex complex = await db.Complexes.FindAsync(key);
            if (complex == null)
            {
                return NotFound();
            }

            patch.Patch(complex);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplexExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(complex);
        }

        // DELETE: odata/Complexes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Complex complex = await db.Complexes.FindAsync(key);
            if (complex == null)
            {
                return NotFound();
            }

            db.Complexes.Remove(complex);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Complexes(5)/Buildings
        [EnableQuery]
        public IQueryable<Building> GetBuildings([FromODataUri] short key)
        {
            return db.Complexes.Where(m => m.complexId == key).SelectMany(m => m.Buildings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplexExists(short key)
        {
            return db.Complexes.Count(e => e.complexId == key) > 0;
        }
    }
}
