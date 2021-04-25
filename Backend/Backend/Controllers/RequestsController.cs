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
using Microsoft.Ajax.Utilities;

namespace Backend.Controllers
{
    public class RequestInserted
    {
        public int employeeID { get; set; }
        public int companyID { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public int atelieID { get; set; }
        public DateTime? expectedDeadlineTime { get; set; }
        public int? totalCost { get; set; }
        public ICollection<MaterialIns> orderedMaterials { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RequestsController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/Requests
        public IQueryable<Request> GetRequest()
        {
            return db.Request;
        }

        // GET: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> GetRequest(int id)
        {
            Request request = await db.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetRequestsByAtelieEmployee(int atelieNum, int? employeeNum)
        {
            var request = db.Request
                .Where(o => o.Employee.atelieID == atelieNum);
            if (employeeNum.HasValue)
                request = request.Where(o => o.employeeID == employeeNum);
            if (!request.ToList().Any())
            {
                return NotFound();
            }

            return Ok(request);
        }

        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.requestID)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        [ResponseType(typeof(Request))]
        public IHttpActionResult PostRequest(RequestInserted reqInst)
        {

            var paymentInsID = db.Payment.Max(p => p.paymentID) + 1;
            db.Payment.Add(new Payment()
            {
                paymentID = paymentInsID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
                employeeID = reqInst.employeeID,
                billNumber = new Random().Next(99999),
                totalCost = reqInst.totalCost,
            });

            var reqPaymentInsID = db.RequestPayment.Max(p => p.paymentID) + 1;
            db.RequestPayment.Add(new RequestPayment()
            {
                requestPaymentID = reqPaymentInsID,
                paymentID = paymentInsID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
            });
            int reqInstID = db.Request.Max(o => o.requestID) + 1;
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            var compID = 0;
            if (!reqInst.companyID.Equals(null))
            {
                compID = reqInst.companyID;
            }
            else
            {
               
                var citID = -1;
                
                compID = db.SupplierCompany.Max(c => c.companyID) + 1;
                var count = db.City.FirstOrDefault(ac => ac.name == reqInst.city);
                if (count != null)
                    citID = count.cityID;
    

                if (citID.Equals(-1))
                {
                    var countrID = db.Country.Max(ois => ois.countryID) + 1;
                    db.Country.Add(new Country()
                    {
                        countryID = countrID,
                        name = reqInst.name,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now,

                    });
                    citID = db.City.Max(ois => ois.cityID) + 1;
                    db.City.Add(new City()
                    {
                        countryID = countrID,
                        cityID = citID,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now,
                        name = reqInst.city
                    });
                }

                db.SupplierCompany.Add(new SupplierCompany()
                {
                    companyID = compID,
                    createDate = DateTime.Now,
                    updateDate = DateTime.Now,
                    name = reqInst.name,
                    rating = new Random().Next(100),
                    address = reqInst.address,
                    cityID = citID,
                });

            }

            var request = db.Request.Add(new Request()
            {
                requestID = reqInstID,
                employeeID = reqInst.employeeID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
                statusID = 1,
                expectedDeadlineTime = reqInst.expectedDeadlineTime,
                companyID = compID,
                requestPaymentID = reqPaymentInsID,

            });
            int reqMatStartID = db.RequestedMaterials.Max(ois => ois.requestedMaterialID);
                reqInst.orderedMaterials.ForEach(om =>
                {
                    reqMatStartID++;
                    db.RequestedMaterials.Add(new RequestedMaterials()
                    {
                        requestedMaterialID = reqMatStartID,
                        materialID = om.materialID,
                        amount = om.materialAmount,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now,
                        requestID = reqInstID,
                    });
                });
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RequestExists(request.requestID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = request.requestID }, request);
        }

        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> DeleteRequest(int id)
        {
            Request request = await db.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Request.Remove(request);
            await db.SaveChangesAsync();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Request.Count(e => e.requestID == id) > 0;
        }
    }
}