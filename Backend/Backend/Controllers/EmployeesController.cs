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

    public class EmployeeDTO
    {
        public int employeeID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime createDate { get; set; }
        public string password { get; set; }
        public string position { get; set; }
        public int atelieID { get; set; }
        public string atelie { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/Employees
        public IQueryable<EmployeeDTO> GetEmployee()
        {
            var employees = db.Employee
                .Include(a => a.Position)
                .Include(a => a.Atelie)
                .Include(a => a.Atelie.City)
                .Include(a => a.Atelie.City.Country)
                .Select(b =>
                new EmployeeDTO()
                {
                    employeeID = b.employeeID,
                    firstname = b.firstname,
                    lastname = b.lastname,
                    email = b.email,
                    position = b.Position.name,
                    phoneNumber = b.phoneNumber,
                    password = b.password,
                    createDate = b.createDate,
                    atelieID = b.atelieID,
                    atelie = b.Atelie.address.ToString() + ", " + b.Atelie.City.name.ToString() + ", " + b.Atelie.City.Country.name.ToString()
                });
            return employees;
        }

        // GET: api/Employees/id
        [ResponseType(typeof(EmployeeDTO))]
        [HttpGet]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {

            var employee = db.Employee
                .Include(a => a.Position)
                .Include(a => a.Atelie)
                .Include(a => a.Atelie.City)
                .Include(a => a.Atelie.City.Country)
                .Select(b =>
                new EmployeeDTO()
                {
                    employeeID = b.employeeID,
                    firstname = b.firstname,
                    lastname = b.lastname,
                    email = b.email,
                    position = b.Position.name,
                    phoneNumber = b.phoneNumber,
                    password = b.password,
                    createDate = b.createDate,
                    atelieID= b.atelieID,
                    atelie = b.Atelie.address.ToString()+ ", "+ b.Atelie.City.name.ToString() + ", "+b.Atelie.City.Country.name.ToString()

                }).SingleOrDefaultAsync(b => b.employeeID == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        // GET: api/Employees/login
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [ActionName("ByNumber")]
        public async Task<IHttpActionResult> GetEmployee(string email,string password)
        {
            var employee = db.Employee
                .Include(a => a.Position)
                .Include(a => a.Atelie)
                .Include(a => a.Atelie.City)
                .Include(a => a.Atelie.City.Country)
                .Select(b =>
                new EmployeeDTO()
                {
                    employeeID = b.employeeID,
                    firstname = b.firstname,
                    lastname = b.lastname,
                    email = b.email,
                    position = b.Position.name,
                    phoneNumber = b.phoneNumber,
                    password = b.password,
                    createDate = b.createDate,
                    atelieID = b.atelieID,
                    atelie = b.Atelie.address.ToString() + ", " + b.Atelie.City.name.ToString() + ", " + b.Atelie.City.Country.name.ToString()

                }).SingleOrDefaultAsync(e => e.email == email && e.password == e.password); ;
 
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
     
        public async Task<IHttpActionResult> GetEmployeeByAtelie(int atelieID)
        {
            var employee = db.Employee
                .Include(a => a.Position)
                .Include(a => a.Atelie)
                .Include(a => a.Atelie.City)
                .Include(a => a.Atelie.City.Country)
                .Select(b =>
                    new EmployeeDTO()
                    {
                        employeeID = b.employeeID,
                        firstname = b.firstname,
                        lastname = b.lastname,
                        email = b.email,
                        position = b.Position.name,
                        phoneNumber = b.phoneNumber,
                        password = b.password,
                        createDate = b.createDate,
                        atelieID = b.atelieID,
                        atelie = b.Atelie.address.ToString() + ", " + b.Atelie.City.name.ToString() + ", " + b.Atelie.City.Country.name.ToString()

                    }).SingleOrDefaultAsync(b => b.atelieID == atelieID);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.employeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employee.Add(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.employeeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.employeeID }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employee.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employee.Count(e => e.employeeID == id) > 0;
        }
    }
}