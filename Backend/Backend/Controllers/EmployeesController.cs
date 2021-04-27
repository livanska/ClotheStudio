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
using WebGrease.Css.Extensions;

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
        public DateTime updateDate { get; set; }
        public string password { get; set; }
        public string position { get; set; }
        public int positionID { get; set; }
        public int atelieID { get; set; }
        public string atelie { get; set; }
        public int ordersCount { get; set; }
        public decimal? ordersCost { get; set; }
        public decimal? avgOrderCost { get; set; }
    }

    public class EmployeeInserted
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int atelieID { get; set; }
        public string password { get; set; }
        public int positionID { get; set; }
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
                    positionID = b.positionID,
                    position = b.Position.name,
                    phoneNumber = b.phoneNumber,
                    password = b.password,
                    createDate = b.createDate,
                    updateDate =b.updateDate,
                    atelieID = b.atelieID,
                    atelie = b.Atelie.address.ToString() + ", " + b.Atelie.City.name.ToString() + ", " + b.Atelie.City.Country.name.ToString(),
                    ordersCount = db.Order.Where(o=>o.employeeID == b.employeeID).Count(),
                    ordersCost = db.Order.Where(o => o.employeeID == b.employeeID).Sum(o=>o.OrderPayment.Payment.totalCost),
                    avgOrderCost = db.Order.Where(o => o.employeeID == b.employeeID).Average(o=> o.OrderPayment.Payment.totalCost)
                }
        );
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
                    positionID = b.positionID,
                    password = b.password,
                    updateDate = b.updateDate,
                    createDate = b.createDate,
                    atelieID= b.atelieID,
                    atelie = b.Atelie.address.ToString()+ ", "+ b.Atelie.City.name.ToString() + ", "+b.Atelie.City.Country.name.ToString(),
                    ordersCount = db.Order.Where(o => o.employeeID == b.employeeID).Count(),
                    ordersCost = db.Order.Where(o => o.employeeID == b.employeeID).Sum(o => o.OrderPayment.Payment.totalCost),
                    avgOrderCost = db.Order.Where(o => o.employeeID == b.employeeID).Average(o => o.OrderPayment.Payment.totalCost)

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
                    positionID = b.positionID,
                    password = b.password,
                    createDate = b.createDate,
                    updateDate = b.updateDate,
                    atelieID = b.atelieID,
                    atelie = b.Atelie.address.ToString() + ", " + b.Atelie.City.name.ToString() + ", " + b.Atelie.City.Country.name.ToString()

                }).SingleOrDefaultAsync(e => e.email == email ); 
 
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
                        positionID = b.positionID,
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
        public async Task<IHttpActionResult> PostEmployee(EmployeeInserted empIns)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = new Employee()
            {
                employeeID = db.Employee.Max(e => e.employeeID) + 1,
                atelieID = empIns.atelieID,
                lastname = empIns.lastname,
                firstname = empIns.firstname,
                phoneNumber = empIns.phoneNumber,
                password = empIns.password,
                positionID = empIns.positionID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
                email = empIns.email
            };

            db.Employee.Add(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {

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