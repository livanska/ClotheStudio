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
using System.Web.Http.Description;
using Backend;

namespace Backend.Controllers
{
    public class StoredMaterialsController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/StoredMaterials
        public IQueryable<StoredMaterials> GetStoredMaterials()
        {
            return db.StoredMaterials;
        }

        // GET: api/StoredMaterials/5
        [ResponseType(typeof(StoredMaterials))]
        public async Task<IHttpActionResult> GetStoredMaterials(int id)
        {
            StoredMaterials storedMaterials = await db.StoredMaterials.FindAsync(id);
            if (storedMaterials == null)
            {
                return NotFound();
            }

            return Ok(storedMaterials);
        }

        // PUT: api/StoredMaterials/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStoredMaterials(int id, StoredMaterials storedMaterials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storedMaterials.storedMaterialID)
            {
                return BadRequest();
            }

            db.Entry(storedMaterials).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoredMaterialsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StoredMaterials
        [ResponseType(typeof(StoredMaterials))]
        public async Task<IHttpActionResult> PostStoredMaterials(StoredMaterials storedMaterials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StoredMaterials.Add(storedMaterials);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StoredMaterialsExists(storedMaterials.storedMaterialID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = storedMaterials.storedMaterialID }, storedMaterials);
        }

        // DELETE: api/StoredMaterials/5
        [ResponseType(typeof(StoredMaterials))]
        public async Task<IHttpActionResult> DeleteStoredMaterials(int id)
        {
            StoredMaterials storedMaterials = await db.StoredMaterials.FindAsync(id);
            if (storedMaterials == null)
            {
                return NotFound();
            }

            db.StoredMaterials.Remove(storedMaterials);
            await db.SaveChangesAsync();

            return Ok(storedMaterials);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoredMaterialsExists(int id)
        {
            return db.StoredMaterials.Count(e => e.storedMaterialID == id) > 0;
        }
    }
}