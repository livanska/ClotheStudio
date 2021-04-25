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
    public class RequestStatusController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/RequestStatus
        public IQueryable<RequestStatus> GetRequestStatus()
        {
            return db.RequestStatus;
        }

        // GET: api/RequestStatus/5
        [ResponseType(typeof(RequestStatus))]
        public async Task<IHttpActionResult> GetRequestStatus(int id)
        {
            RequestStatus requestStatus = await db.RequestStatus.FindAsync(id);
            if (requestStatus == null)
            {
                return NotFound();
            }

            return Ok(requestStatus);
        }

        // PUT: api/RequestStatus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestStatus(int id, RequestStatus requestStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestStatus.requestStatusID)
            {
                return BadRequest();
            }

            db.Entry(requestStatus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestStatusExists(id))
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

        // POST: api/RequestStatus
        [ResponseType(typeof(RequestStatus))]
        public async Task<IHttpActionResult> PostRequestStatus(RequestStatus requestStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestStatus.Add(requestStatus);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RequestStatusExists(requestStatus.requestStatusID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = requestStatus.requestStatusID }, requestStatus);
        }

        // DELETE: api/RequestStatus/5
        [ResponseType(typeof(RequestStatus))]
        public async Task<IHttpActionResult> DeleteRequestStatus(int id)
        {
            RequestStatus requestStatus = await db.RequestStatus.FindAsync(id);
            if (requestStatus == null)
            {
                return NotFound();
            }

            db.RequestStatus.Remove(requestStatus);
            await db.SaveChangesAsync();

            return Ok(requestStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestStatusExists(int id)
        {
            return db.RequestStatus.Count(e => e.requestStatusID == id) > 0;
        }
    }
}