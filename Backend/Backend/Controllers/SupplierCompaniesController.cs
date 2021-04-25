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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SupplierCompanyDTO
    {
        public int companyID { get; set; }
        public int rating { get; set; }
        public string name { get; set; }
        public string location { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SupplierCompaniesController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/SupplierCompanies
        public IQueryable<SupplierCompanyDTO> GetSupplierCompany()
        {
            var companies = db.SupplierCompany
                .Include(c => c.City)
                .Include(c => c.City.Country)
                .Select(c => new SupplierCompanyDTO()
                {
                    companyID = c.companyID,
                    rating = c.rating,
                    name = c.name,
                    location = c.address.ToString() + ", " + c.City.name.ToString() + ", " +
                               c.City.Country.name.ToString(),
                });
            return companies;
        }

        // GET: api/SupplierCompanies/5
        [ResponseType(typeof(SupplierCompanyDTO))]
        public async Task<IHttpActionResult> GetSupplierCompany(int id)
        {
            var company = db.SupplierCompany
                .Include(c => c.City)
                .Include(c => c.City.Country)
                .Select(c => new SupplierCompanyDTO()
                {
                    companyID = c.companyID,
                    rating = c.rating,
                    name = c.name,
                    location = c.address.ToString() + ", " + c.City.name.ToString() + ", " +
                               c.City.Country.name.ToString(),
                }).SingleOrDefaultAsync(c => c.companyID == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        //Get api/SupplierCompanies/name
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]

        [ResponseType(typeof(SupplierCompany))]
        public IHttpActionResult GetSupplierCompany(string name)
        {
            SupplierCompany company = db.SupplierCompany.ToList().Find(c => c.name== name);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // PUT: api/SupplierCompanies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSupplierCompany(int id, SupplierCompany supplierCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplierCompany.companyID)
            {
                return BadRequest();
            }

            db.Entry(supplierCompany).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierCompanyExists(id))
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

        // POST: api/SupplierCompanies
        [ResponseType(typeof(SupplierCompany))]
        public async Task<IHttpActionResult> PostSupplierCompany(SupplierCompany supplierCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SupplierCompany.Add(supplierCompany);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupplierCompanyExists(supplierCompany.companyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = supplierCompany.companyID }, supplierCompany);
        }

        // DELETE: api/SupplierCompanies/5
        [ResponseType(typeof(SupplierCompany))]
        public async Task<IHttpActionResult> DeleteSupplierCompany(int id)
        {
            SupplierCompany supplierCompany = await db.SupplierCompany.FindAsync(id);
            if (supplierCompany == null)
            {
                return NotFound();
            }

            db.SupplierCompany.Remove(supplierCompany);
            await db.SaveChangesAsync();

            return Ok(supplierCompany);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierCompanyExists(int id)
        {
            return db.SupplierCompany.Count(e => e.companyID == id) > 0;
        }
    }
}