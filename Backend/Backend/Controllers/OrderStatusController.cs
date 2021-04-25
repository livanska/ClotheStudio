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
    public class OrderStatusController : ApiController
    {
        private SewingAtelie db = new SewingAtelie();

        // GET: api/OrderStatus
        public IQueryable<OrderStatus> GetOrderStatus()
        {
            return db.OrderStatus;
        }

        // GET: api/OrderStatus/5
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> GetOrderStatus(int id)
        {
            OrderStatus orderStatus = await db.OrderStatus.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        // PUT: api/OrderStatus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderStatus(int id, OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderStatus.orderStatusID)
            {
                return BadRequest();
            }

            db.Entry(orderStatus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // POST: api/OrderStatus
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> PostOrderStatus(OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderStatus.Add(orderStatus);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderStatusExists(orderStatus.orderStatusID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderStatus.orderStatusID }, orderStatus);
        }

        // DELETE: api/OrderStatus/5
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> DeleteOrderStatus(int id)
        {
            OrderStatus orderStatus = await db.OrderStatus.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            db.OrderStatus.Remove(orderStatus);
            await db.SaveChangesAsync();

            return Ok(orderStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderStatusExists(int id)
        {
            return db.OrderStatus.Count(e => e.orderStatusID == id) > 0;
        }
    }
}