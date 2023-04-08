using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement_System_Demo.Data;
using UserManagement_System_Demo.Models;

namespace UserManagement_System_Demo.APIControllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(UserRoles.Admin)]
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                List<OrderVM> neworderlist = new List<OrderVM>();
                var order = (from a in _db.Orders.Where(x => x.IsDeleted == false)
                             select new
                             {

                                 a


                             }).Distinct();
                foreach (var ordr in order)
                {
                    OrderVM myorder = new OrderVM();
                    {
                        myorder.Id = ordr.a.Id;
                        myorder.DateCreated = ordr.a.DateCreated;
                        myorder.Amount = ordr.a.Amount;
                        myorder.Quantity = ordr.a.Quantity;
                        if (ordr.a.IsCompleted == true)
                        {
                            myorder.Status = "Process Completed";
                        }
                        else
                        {
                            myorder.Status = "Not Completed";
                        }
                        if (ordr.a.IsCancelled == true)
                        {
                            myorder.OrderStatus = "Process has been cancelled";
                        }
                        else
                        {
                            myorder.OrderStatus = "Not Cancelled";
                        }
                        neworderlist.Add(myorder);
                    }


                };
                return Ok(neworderlist);


            }
            catch (Exception e)
            {
                var message = "There has been an error. Kindly try again";
                return Ok(message);
            }

        }


        [Authorize]
        [HttpGet]
        [Route("GetOrdersTest/{date?}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersTest(string date)
        {
            try
            {
                List<OrderVM> neworderlist = new List<OrderVM>();
                var order = (from a in _db.Orders.Where(x => x.DateCreated == Convert.ToDateTime(date) && x.IsDeleted == false)
                             select new
                             {

                                 a


                             }).Distinct();
                foreach (var ordr in order)
                {
                    OrderVM myorder = new OrderVM();
                    {
                        myorder.Id = ordr.a.Id;
                        myorder.DateCreated = ordr.a.DateCreated;
                        myorder.Amount = ordr.a.Amount;
                        myorder.Quantity = ordr.a.Quantity;
                        if (ordr.a.IsCompleted == true)
                        {
                            myorder.Status = "Process Completed";
                        }
                        else
                        {
                            myorder.Status = "Not Completed";
                        }
                        if (ordr.a.IsCancelled == true)
                        {
                            myorder.OrderStatus = "Process has been cancelled";
                        }
                        else
                        {
                            myorder.OrderStatus = "Not Cancelled";
                        }
                        neworderlist.Add(myorder);
                    }


                };
                return Ok(neworderlist);


            }
            catch (Exception e)
            {
                var message = "There has been an error. Kindly try again";
                return Ok(message);
            }

        }

        //GET: api/Orders/5
        [Authorize]
        [HttpGet]
        [Route("GetOrders/{amount?}")]
        public async Task<ActionResult<Order>> GetMyOrder(double amount)
        {
            try
            {
                List<OrderVM> neworderlist = new List<OrderVM>();
                var order = (from a in _db.Orders.Where(x => x.Amount == amount && x.IsDeleted == false)
                             select new
                             {

                                 a


                             });
                foreach (var ordr in order)
                {
                    OrderVM myorder = new OrderVM();
                    {
                        myorder.Id = ordr.a.Id;
                        myorder.DateCreated = ordr.a.DateCreated;
                        myorder.Amount = ordr.a.Amount;
                        myorder.Quantity = ordr.a.Quantity;
                        if (ordr.a.IsCompleted == true)
                        {
                            myorder.Status = "Process Completed";
                        }
                        else
                        {
                            myorder.Status = "Not Completed";
                        }
                        if (ordr.a.IsCancelled == true)
                        {
                            myorder.OrderStatus = "Process has been cancelled";
                        }
                        else
                        {
                            myorder.OrderStatus = "Not Cancelled";
                        }
                        neworderlist.Add(myorder);
                    }


                };
                return Ok(neworderlist.LastOrDefault());


            }
            catch (Exception e)
            {
                var message = "There has been an error. Kindly try again";
                return Ok(message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetOrderById/{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                List<OrderVM> neworderlist = new List<OrderVM>();
                var order = (from a in _db.Orders.Where(x => x.Id == id && x.IsDeleted == false)
                             select new
                             {

                                 a


                             });
                foreach (var ordr in order)
                {
                    OrderVM myorder = new OrderVM();
                    {
                        myorder.Id = ordr.a.Id;
                        myorder.DateCreated = ordr.a.DateCreated;
                        myorder.Amount = ordr.a.Amount;
                        myorder.Quantity = ordr.a.Quantity;
                        if (ordr.a.IsCompleted == true)
                        {
                            myorder.Status = "Process Completed";
                        }
                        else
                        {
                            myorder.Status = "Not Completed";
                        }
                        if (ordr.a.IsCancelled == true)
                        {
                            myorder.OrderStatus = "Process has been cancelled";
                        }
                        else
                        {
                            myorder.OrderStatus = "Not Cancelled";
                        }
                        neworderlist.Add(myorder);
                    }


                };
                return Ok(neworderlist.LastOrDefault());


            }
            catch (Exception e)
            {
                var message = "There has been an error. Kindly try again";
                return Ok(message);
            }
        }

        // PUT: api/Orders/5
        //  To protect from overposting attacks, 
        // see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut]
        [Route("OrderUpdate")]
        public async Task<IActionResult> PutOrder(int id, int? quantity,
            double? amount, bool? isCompleted)
        {
            var myorder = _db.Orders.Where(x => x.Id == id).FirstOrDefault();

            if (myorder == null)
            {
                var errormsg = "Order does not exist";
                return Ok(errormsg);
            }

            var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);

            order.Quantity = quantity;
            order.Amount = (double)amount;
            order.IsCompleted = isCompleted;
            order.DateUpdated = DateTime.Now;

            _db.Update(order);



            _db.Entry(order).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                var successmsg = "You have updated your record successfully";
                return Ok(successmsg);
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

            return NoContent();
        }


        [Authorize]
        [HttpPut]
        [Route("Remove")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            var myorder = _db.Orders.Where(x => x.Id == id).FirstOrDefault();

            if (myorder == null)
            {
                var errormsg = "Order does not exist";
                return Ok(errormsg);
            }

            var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);

            order.IsDeleted = true;
            order.DateUpdated = DateTime.Now;

            _db.Update(order);



            _db.Entry(order).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                var successmsg = "You have deleted the record successfully";
                return Ok(successmsg);
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

            return NoContent();
        }

        //POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(UserRoles.User)]
        [HttpPost]
        [Route("AddOrders")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [Authorize(UserRoles.Admin)]
        [HttpDelete("{id}")]
        //[Route(OrderDelete)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.IsDeleted = true;
            order.DateUpdated = DateTime.Now;
            _db.Update(order);
            await _db.SaveChangesAsync();

            var successmsg = "You have deleted the record successfully";
            return Ok(successmsg);
        }

        private bool OrderExists(int id)
        {
            return _db.Orders.Any(e => e.Id == id);
        }
    }
}
