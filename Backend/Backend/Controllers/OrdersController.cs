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
    public class OrderDTO
    {
        public int orderID { get; set; }
        public int employeeID { get; set; }
        public string employee { get; set; }
        public int customerID { get; set; }
        public string customer { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public DateTime createDate { get; set; }
        public DateTime expectedDeadlineTime { get; set; }
        public DateTime? realReceivingTime { get; set; }
       // public int? orderPaymentID { get; set; }
        public int? totalCost { get; set; }
        public ICollection<OrderedItems> orderedItems { get; set; }

    }

    public class OrderInserted
    {
        public int employeeID { get; set; }
        public int? customerID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phoneNumber { get; set; }
        public int statusID { get; set; }
        public DateTime expectedDeadlineTime { get; set; }
        public int? totalCost { get; set; }
        public ICollection<OrderedItems> orderedItems { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/Orders
        public IQueryable<Order> GetOrder()
        {
            return db.Order;
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            Order order = db.Order.Find(id);


            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetOrdersByAtelieEmployee(int atelieNum, int? employeeNum)
        {
            var order = db.Order
                .Where(o => o.Employee.atelieID == atelieNum); 
            if (employeeNum.HasValue)
                order = order.Where(o => o.employeeID == employeeNum);
            if (!order.ToList().Any())
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.orderID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
       /* [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(OrderInserted orderIns)
        {
            int orderInsID = db.Order.Max(o => o.orderID) + 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*orderIns.orderedItems.ForEach(oi =>
            {
                int ordItemID = db.OrderedItems.Max(ois => ois.orderedItemID) + 1;
                db.OrderedItems.Add(
                    new OrderedItems()
                    {
                        orderedItemID = ordItemID,
                        orderID = orderInsID,
                        serviceID = oi.serviceID,
                        description = oi.description,
                        employeeID = orderIns.employeeID,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now,
                    }
                );
                db.RequiredMaterialsForOrderedItem.Add(new RequiredMaterialsForOrderedItem()
                {
                    orderedItemID = ordItemID,
                    materialID = oi.,
                    createDate = DateTime.Now,
                    updateDate = DateTime.Now,
                    amount = orderInsID
                })
            });




            var order = orderIns;

           // db.Order.Add(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.orderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order.orderID }, order);
        }*/

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.orderID == id) > 0;
        }
    }
}