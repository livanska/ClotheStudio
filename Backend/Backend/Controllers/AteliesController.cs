using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Backend;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AteliesController : ApiController
    {
        public class AtelieDTO
        {
            public int atelieID { get; set; }
            public string location { get; set; }
            public DateTime createDate { get; set; }
            public decimal? totalIncome { get; set; }
            public decimal? totalOutgoings { get; set; }

        }
        private SewingAtelie db = new SewingAtelie();

        // GET: api/Atelies
        public IQueryable<Atelie> GetAtelie()
        {
            return db.Atelie;
        }

        // GET: api/Atelies/5
        [ResponseType(typeof(Atelie))]
        public IHttpActionResult GetAtelie(int id)
        {
            
            var atelie = db.Atelie.Include(a=>a.Employee).Select(b =>
                new AtelieDTO()
                {
                    atelieID = b.atelieID,
                    location = b.address.ToString() + ", " + b.City.name.ToString() + ", " + b.City.Country.name.ToString(),
                    createDate = b.createDate,
                    totalIncome =db.Order.Where(a=>a.Employee.atelieID==id).Sum(o => o.OrderPayment.Payment.totalCost),
                    totalOutgoings = db.Request.Where(a => a.Employee.atelieID == id).Sum(o => o.RequestPayment.Payment.totalCost),

                }).SingleOrDefaultAsync(b => b.atelieID == id);
            if (atelie == null)
            {
                return NotFound();
            }

            return Ok(atelie);
        }

        // PUT: api/Atelies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAtelie(int id, Atelie atelie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != atelie.atelieID)
            {
                return BadRequest();
            }

            db.Entry(atelie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtelieExists(id))
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

        // POST: api/Atelies
        [ResponseType(typeof(Atelie))]
        public IHttpActionResult PostAtelie(Atelie atelie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Atelie.Add(atelie);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AtelieExists(atelie.atelieID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = atelie.atelieID }, atelie);
        }

        // DELETE: api/Atelies/5
        [ResponseType(typeof(Atelie))]
        public IHttpActionResult DeleteAtelie(int id)
        {
            Atelie atelie = db.Atelie.Find(id);
            if (atelie == null)
            {
                return NotFound();
            }

            db.Atelie.Remove(atelie);
            db.SaveChanges();

            return Ok(atelie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AtelieExists(int id)
        {
            return db.Atelie.Count(e => e.atelieID == id) > 0;
        }
    }
}