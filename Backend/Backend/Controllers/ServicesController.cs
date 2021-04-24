using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Backend;

namespace Backend.Controllers
{
    public class ServicesDTO
    {
        public int serviceID { get; set; }
        public string name { get; set; }
        public TimeSpan duration { get; set; }
        public decimal workCost { get; set; }
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServicesController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/Services
        public IQueryable<ServicesDTO> GetServices()
        {
            var services = db.Services.Include(s => s.ClotheType).Include(s => s.ServiceType).Select(s => new ServicesDTO()
            {
                serviceID = s.serviceID,
                name=s.ServiceType.name.ToString()+" "+s.ClotheType.name,
                duration = s.duration,
                workCost = s.workCost
            });
       
            return services;
        }

        // GET: api/Services/5
        [ResponseType(typeof(Services))]
        public async Task<IHttpActionResult> GetServices(int id)
        {
            var service = db.Services.Include(s => s.ClotheType).Include(s => s.ServiceType).Select(s => new ServicesDTO()
            {
                serviceID = s.serviceID,
                name = s.ServiceType.name.ToString() + " " + s.ClotheType.name,
                duration = s.duration,
                workCost = s.workCost
            }).SingleOrDefaultAsync(s => s.serviceID == id);
            if (service.Equals(null))
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutServices(int id, Services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != services.serviceID)
            {
                return BadRequest();
            }

            db.Entry(services).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(id))
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

        // POST: api/Services
        [ResponseType(typeof(Services))]
        public async Task<IHttpActionResult> PostServices(Services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Services.Add(services);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServicesExists(services.serviceID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = services.serviceID }, services);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Services))]
        public async Task<IHttpActionResult> DeleteServices(int id)
        {
            Services services = await db.Services.FindAsync(id);
            if (services == null)
            {
                return NotFound();
            }

            db.Services.Remove(services);
            await db.SaveChangesAsync();

            return Ok(services);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicesExists(int id)
        {
            return db.Services.Count(e => e.serviceID == id) > 0;
        }
    }
}