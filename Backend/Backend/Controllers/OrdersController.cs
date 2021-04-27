using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
        public DateTime? expectedDeadlineTime { get; set; }
        public DateTime? realReceivingTime { get; set; }
        // public int? orderPaymentID { get; set; }
        public decimal? totalCost { get; set; }
        public ICollection<OrderedItems> orderedItems { get; set; }

    }

    public class MaterialIns
    {
        public int materialID { get; set; }
        public int materialAmount { get; set; }
    }

    public class OrderedItemInserted
    {
        public ICollection<MaterialIns> reqMaterials { get; set; }
        public int serviceID { get; set; }
        public string description { get; set; }

    }
    public class OrderInserted
    {
        public int employeeID { get; set; }
        public int customerID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phoneNumber { get; set; }
        public int statusID { get; set; }
        public int atelieID { get; set; }
        public DateTime? expectedDeadlineTime { get; set; }
        public decimal? totalCost { get; set; }
        public ICollection<OrderedItemInserted> orderedItems { get; set; }
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
            order.OrderStatus = db.OrderStatus.Where(o => o.orderStatusID == order.statusID).First();
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
        [ResponseType(typeof(OrderInserted))]
        public IHttpActionResult PostOrder(OrderInserted orderIns)
        {
            var paymentInsID = db.Payment.Max(p => p.paymentID) + 1;
            db.Payment.Add(new Payment()
            {
                paymentID = paymentInsID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
                billNumber = db.Payment.Max(o => o.billNumber) + 1,
            totalCost = orderIns.totalCost,
            });

            var ordPaymentInsID = db.OrderPayment.Max(p => p.paymentID) + 1;
            db.OrderPayment.Add(new OrderPayment()
            {
                orderPaymentID = ordPaymentInsID,
                paymentID = paymentInsID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
            });
            int orderInsID = db.Order.Max(o => o.orderID) + 1;
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            var custID = 0;
            if (orderIns.customerID != null && orderIns.customerID != -1)
            {
                custID = orderIns.customerID;
                var discount = db.Customer.Where(c => c.customerID == custID).First().discount;
                var ordersCount = db.Order.Where(o => o.customerID == custID).Count();
                var newDiscount = discount;
                if ((discount < 10) && ((ordersCount + 1) % 10 == 0))
                    newDiscount++;
                else if((discount < 20 && discount >= 10 ) && ((ordersCount + 1) % 50 == 0))
                    newDiscount++;
                else if((discount >=20 ) && ((ordersCount + 1) % 100 == 0))
                    newDiscount++;
                if (newDiscount != discount)
                {
                    var cust = db.Customer.Where(c => c.customerID == custID).First();
                    cust.discount = newDiscount;
                    db.Entry(cust).State = EntityState.Modified;


                }

            }
            else {
            custID = db.Customer.Max(c => c.customerID) + 1;
                db.Customer.Add(new Customer()
                {
                    customerID = custID,
                    createDate = DateTime.Now,
                    updateDate = DateTime.Now,
                    phoneNumber = orderIns.phoneNumber,
                    discount = 0,
                    firstname = orderIns.firstname,
                    lastname = orderIns.lastname


                });
            }

            var order = db.Order.Add(new Order()
            {
                orderID = orderInsID,
                employeeID = orderIns.employeeID,
                createDate = DateTime.Now,
                updateDate = DateTime.Now,
                statusID = 1,
                expectedDeadlineTime = orderIns.expectedDeadlineTime,
                customerID = custID,
                orderPaymentID = ordPaymentInsID,

            });
            int reqMatID = db.RequiredMaterialsForOrderedItem.Max(rm => rm.requiredMaterialID)+1;
            int ordItemStartID = db.OrderedItems.Max(ois => ois.orderedItemID) ;
            orderIns.orderedItems.ForEach(oi =>
            {
                ordItemStartID++;
                int ordItemID = ordItemStartID;
                db.OrderedItems.Add(new OrderedItems()
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
               
                oi.reqMaterials.ForEach(m =>
                {
                    var stormatID = -1;
                    var matExists = db.StoredMaterials.Any(sm =>
                        sm.atelierID == orderIns.atelieID && sm.materialID == m.materialID);
                    if(matExists)
                    {
                        stormatID = db.StoredMaterials.Where(sm => sm.atelierID == orderIns.atelieID && sm.materialID == m.materialID).First().storedMaterialID;
                        var stormat = db.StoredMaterials.Find(stormatID);
                        if (stormat.amountLeft >= m.materialAmount)
                        {
                            
                            stormat.amountLeft -= m.materialAmount;
                            db.Entry(stormat).State = EntityState.Modified;

                        }
                         else stormatID = -1;


                    }


                  
                    db.RequiredMaterialsForOrderedItem.Add(new RequiredMaterialsForOrderedItem()
                    {
                        requiredMaterialID = reqMatID++,
                        materialID = m.materialID,
                        amount = m.materialAmount,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now,
                        orderedItemID = ordItemID,
                    });

                    if (stormatID != -1)
                    {
                        var ReqM = db.RequiredMaterialsForOrderedItem.Find(reqMatID);
                        ReqM.storedMaterialID = stormatID;

                    } 
                    
                
                });
            });
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(orderInsID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderInsID }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            var ordItemsToRem = db.OrderedItems.Where(oi => oi.orderID == id);
            ordItemsToRem.ForEach(oi =>
            {
               var reqMat = db.RequiredMaterialsForOrderedItem.Where(rmoi => rmoi.orderedItemID == oi.orderedItemID);
                reqMat.ForEach(rm => db.RequiredMaterialsForOrderedItem.Remove(rm));
                db.OrderedItems.Remove(oi);
            });


            var ordPaymnet = db.OrderPayment.Where(p => p.orderPaymentID == order.orderPaymentID).First();
            var paymnet = ordPaymnet.Payment;

            db.Payment.Remove(paymnet);
            db.OrderPayment.Remove(ordPaymnet);
            


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