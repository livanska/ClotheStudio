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
    public class OrderedItemDTO
    {
        public int orderedItemID { get; set; }
        public string clotheType { get; set; }
        public string service { get; set; }
        public DateTime createDate { get; set; }
        public string description { get; set; }
        public DateTime doneTime { get; set; }
        public ICollection<RequiredMaterialsForOrderedItem> requiredMaterials { get; set; }
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderedItemsController : ApiController
    {

        private SewingAtelie db = new SewingAtelie();
        
        // GET: api/OrderedItems
        public IQueryable<OrderedItems> GetOrderedItems()
        {
            return db.OrderedItems;
        }

        // GET: api/OrderedItems/5
        [ResponseType(typeof(OrderedItems))]
        public async Task<IHttpActionResult> GetOrderedItems(int id)
        {
            var orderedItems = await db.OrderedItems
                .Include(oi=> oi.Services.ServiceType)
                .Select(oi => 
                    new OrderedItemDTO ()
                    {  service = oi.Services.ServiceType.name,
                        clotheType = oi.Services.ClotheType.name,
                        createDate = oi.createDate,
                        description = oi.description,
                        orderedItemID =oi.orderedItemID,
                        doneTime =oi.doneTime,
                        requiredMaterials = oi.RequiredMaterialsForOrderedItem.Where(r => r.orderedItemID == id).ToList()

                    }).SingleOrDefaultAsync(oi=> oi.orderedItemID == id);
            if (orderedItems == null)
            {
                return NotFound();
            }

            return Ok(orderedItems);
        }

        // PUT: api/OrderedItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderedItems(int id, OrderedItems orderedItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderedItems.orderedItemID)
            {
                return BadRequest();
            }

            db.Entry(orderedItems).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderedItemsExists(id))
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

        // POST: api/OrderedItems
        [ResponseType(typeof(OrderedItems))]
        public async Task<IHttpActionResult> PostOrderedItems(OrderedItems orderedItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderedItems.Add(orderedItems);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderedItemsExists(orderedItems.orderedItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderedItems.orderedItemID }, orderedItems);
        }

        // DELETE: api/OrderedItems/5
        [ResponseType(typeof(OrderedItems))]
        public async Task<IHttpActionResult> DeleteOrderedItems(int id)
        {
            OrderedItems orderedItems = await db.OrderedItems.FindAsync(id);
            if (orderedItems == null)
            {
                return NotFound();
            }

            db.OrderedItems.Remove(orderedItems);
            await db.SaveChangesAsync();

            return Ok(orderedItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderedItemsExists(int id)
        {
            return db.OrderedItems.Count(e => e.orderedItemID == id) > 0;
        }
    }
}