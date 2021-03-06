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

    public class CustomersController : ApiController
    {
        public class CustomerDTO 
        {
            public int customerID { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string phoneNumber { get; set; }
            public int? discount { get; set; }
            public DateTime createDate { get; set; }
            public DateTime updateDate { get; set; }
            public int? ordersCount  { get; set; }
        }

        private SewingAtelie db = new SewingAtelie();

        // GET: api/Customers
        public IQueryable<Customer> GetCustomer()
        {
            return db.Customer;
    
        }

        // GET: api/Customers/5

        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var cust = db.Customer
                .Select(customer=> 
                    new CustomerDTO()
                {
                    createDate = customer.createDate,
                    updateDate = customer.updateDate,
                    firstname = customer.firstname,
                    lastname = customer.lastname,
                    phoneNumber = customer.phoneNumber,
                    discount = customer.discount,
                    customerID = customer.customerID,
                    ordersCount = db.Order.Where(o => o.customerID == id).Count()

                }).SingleOrDefaultAsync(b => b.customerID == id);

            return Ok(cust);
        }


        // GET: api/CustomersByNumber/567-876-232
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
       // [ActionName("ByNumber")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(string number)
        {
            Customer customer = db.Customer.ToList().Find( c => c.phoneNumber == number);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        // PUT: api/Customers/5
        [ResponseType(typeof(void))]

        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.customerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
    
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customer.Add(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.customerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.customerID }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]

        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customer.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customer.Count(e => e.customerID == id) > 0;
        }
    }
}