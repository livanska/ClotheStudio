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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Backend;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MaterialsController : ApiController
    {
        public class MaterialDTO
        {
            public int materialID { get; set; }
            public int materialTypeID { get; set; }
            public int colorID { get; set; }
            public decimal costPerUnit { get; set; }
            public string color { get; set; }
            public string name { get; set; }
        }

        private SewingAtelie db = new SewingAtelie();

        // GET: api/Materials
        public IQueryable<MaterialDTO> GetMaterial()
        {
            var materials=  db.Material.Include(m=>m.Color).Include(m=>m.MaterialType).Select(m=> 
                new MaterialDTO()
            {
                    materialID = m.materialID,
                    materialTypeID = m.materialTypeID,
                    colorID = m.colorID,
                    costPerUnit = m.costPerUnit,
                    color = m.Color.name,
                    name = m.MaterialType.name

            });
            return materials;
        }

        // GET: api/Materials/5
        [ResponseType(typeof(Material))]
        public async Task<IHttpActionResult> GetMaterial(int id)
        {
            var material = db.Material.Include(m => m.Color).Include(m => m.MaterialType).Select(m =>
                new MaterialDTO()
                {
                    materialID = m.materialID,
                    materialTypeID = m.materialTypeID,
                    colorID = m.colorID,
                    costPerUnit = m.costPerUnit,
                    color = m.Color.name,
                    name = m.MaterialType.name

                }).SingleOrDefaultAsync(m=> m.materialID == id);
            if (material.Equals(null))
            {
                return NotFound();
            }
            return Ok(material);
        }

        // PUT: api/Materials/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMaterial(int id, Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != material.materialID)
            {
                return BadRequest();
            }

            db.Entry(material).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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

        // POST: api/Materials
        [ResponseType(typeof(Material))]
        public async Task<IHttpActionResult> PostMaterial(Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Material.Add(material);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaterialExists(material.materialID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = material.materialID }, material);
        }

        // DELETE: api/Materials/5
        [ResponseType(typeof(Material))]
        public async Task<IHttpActionResult> DeleteMaterial(int id)
        {
            Material material = await db.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            db.Material.Remove(material);
            await db.SaveChangesAsync();

            return Ok(material);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialExists(int id)
        {
            return db.Material.Count(e => e.materialID == id) > 0;
        }
    }
}