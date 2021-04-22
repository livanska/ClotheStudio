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
    public class PositionsController : ApiController
    {
        
        private SewingAtelie db = new SewingAtelie();

        // GET: api/Positions
        public IQueryable<Position> GetPosition()
        {
            return db.Position;
        }

        // GET: api/Positions/5
        [ResponseType(typeof(Position))]
        public async Task<IHttpActionResult> GetPosition(int id)
        {
            Position position = await db.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // PUT: api/Positions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPosition(int id, Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.positionID)
            {
                return BadRequest();
            }

            db.Entry(position).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        [ResponseType(typeof(Position))]
        public async Task<IHttpActionResult> PostPosition(Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Position.Add(position);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PositionExists(position.positionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = position.positionID }, position);
        }

        // DELETE: api/Positions/5
        [ResponseType(typeof(Position))]
        public async Task<IHttpActionResult> DeletePosition(int id)
        {
            Position position = await db.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            db.Position.Remove(position);
            await db.SaveChangesAsync();

            return Ok(position);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PositionExists(int id)
        {
            return db.Position.Count(e => e.positionID == id) > 0;
        }
    }
}