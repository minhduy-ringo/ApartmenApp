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
    builder.EntitySet<Building>("Buildings");
    builder.EntitySet<Complex>("Complexes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BuildingsController : ODataController
    {
        private ComplexContext db = new ComplexContext();

        // GET: odata/Buildings
        [EnableQuery]
        public IQueryable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        // GET: odata/Buildings(5)
        [EnableQuery]
        public SingleResult<Building> GetBuilding([FromODataUri] short key)
        {
            return SingleResult.Create(db.Buildings.Where(building => building.complexId == key));
        }

        // PUT: odata/Buildings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Building> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Building building = await db.Buildings.FindAsync(key);
            if (building == null)
            {
                return NotFound();
            }

            patch.Put(building);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(building);
        }

        // POST: odata/Buildings
        public async Task<IHttpActionResult> Post(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Buildings.Add(building);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BuildingExists(building.complexId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(building);
        }

        // PATCH: odata/Buildings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Building> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Building building = await db.Buildings.FindAsync(key);
            if (building == null)
            {
                return NotFound();
            }

            patch.Patch(building);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(building);
        }

        // DELETE: odata/Buildings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Building building = await db.Buildings.FindAsync(key);
            if (building == null)
            {
                return NotFound();
            }

            db.Buildings.Remove(building);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Buildings(5)/Complex
        [EnableQuery]
        public SingleResult<Complex> GetComplex([FromODataUri] short key)
        {
            return SingleResult.Create(db.Buildings.Where(m => m.complexId == key).Select(m => m.Complex));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingExists(short key)
        {
            return db.Buildings.Count(e => e.complexId == key) > 0;
        }
    }
}
