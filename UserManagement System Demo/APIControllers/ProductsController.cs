using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement_System_Demo.Data;
using UserManagement_System_Demo.Models;

namespace UserManagement_System_Demo.APIControllers
{

    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Order>>> GetProducts()
        {
            try
            {
                List<ProvductsVM> newproductslist = new List<ProvductsVM>();
                var products = (from a in _db.products.Where(x => x.IsDeleted == false)
                             select new
                             {

                                 a


                             }).Distinct();
                foreach (var prdt in products)
                {
                    ProvductsVM myproducts = new ProvductsVM();
                    {
                        myproducts.ProductName = prdt.a.ProductName;
                        myproducts.ProductDescription = prdt.a.ProductDescription;
                        myproducts.ProductPrice = prdt.a.ProductPrice;
                        myproducts.Manufacturer = prdt.a.Manufacturer;
                        myproducts.ExpirationDate = prdt.a.ExpirationDate;
                        newproductslist.Add(myproducts);
                    }


                };
                return Ok(newproductslist);


            }
            catch (Exception e)
            {
                var message = "There has been an error. Kindly try again";
                return Ok(message);
            }

        }


        [Authorize]
        [HttpGet]
        [Route("GetProductsByDate/{date?}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetProductsByDate(string date)
        {
            try
            {
                List<ProvductsVM> newproductslist = new List<ProvductsVM>();
                var products = (from a in _db.products.Where(x => x.DateCreated == Convert.ToDateTime(date) && x.IsDeleted == false)
                             select new
                             {

                                 a


                             }).Distinct();
                foreach (var prdt in products)
                {
                    ProvductsVM myproducts = new ProvductsVM();
                    {
                        myproducts.ProductName = prdt.a.ProductName;
                        myproducts.ProductDescription = prdt.a.ProductDescription;
                        myproducts.ProductPrice = prdt.a.ProductPrice;
                        myproducts.Manufacturer = prdt.a.Manufacturer;
                        myproducts.ExpirationDate = prdt.a.ExpirationDate;
                        newproductslist.Add(myproducts);
                    }


                };
                return Ok(newproductslist);



         


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
        [Route("GetProductsByAmount/{amount?}")]
        public async Task<ActionResult<Order>> GetPproductsByAmount(double amount)
        {
            try
            {
                List<ProvductsVM> newproductslist = new List<ProvductsVM>();
                var products = (from a in _db.products.Where(x => x.ProductPrice == amount && x.IsDeleted == false)
                                select new
                                {

                                    a


                                }).Distinct();
                foreach (var prdt in products)
                {
                    ProvductsVM myproducts = new ProvductsVM();
                    {
                        myproducts.ProductName = prdt.a.ProductName;
                        myproducts.ProductDescription = prdt.a.ProductDescription;
                        myproducts.ProductPrice = prdt.a.ProductPrice;
                        myproducts.Manufacturer = prdt.a.Manufacturer;
                        myproducts.ExpirationDate = prdt.a.ExpirationDate;
                        newproductslist.Add(myproducts);
                    }


                };
                return Ok(newproductslist);


            }
            catch (Exception e)
            {
                var message = "There has been an error. Kindly try again";
                return Ok(message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<ActionResult<Order>> GetProductById(int id)
        {
            try
            {

                var product = _db.products.Where(x => x.Id == id).FirstOrDefault();

                /*List<ProvductsVM> newproductslist = new List<ProvductsVM>();
                var products = (from a in _db.products.Where(x => x.Id == id && x.IsDeleted == false)
                                select new
                                {

                                    a


                                }).Distinct();
                foreach (var prdt in products)
                {
                    ProvductsVM myproducts = new ProvductsVM();
                    {
                        myproducts.ProductName = prdt.a.ProductName;
                        myproducts.ProductDescription = prdt.a.ProductDescription;
                        myproducts.ProductPrice = prdt.a.ProductPrice;
                        myproducts.Manufacturer = prdt.a.Manufacturer;
                        myproducts.ExpirationDate = prdt.a.ExpirationDate;
                        newproductslist.Add(myproducts);
                    }


                };*/
                return Ok(product);


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
        [Route("ProductUpdate")]
        public async Task<IActionResult> PutProduct([FromBody] Product myproduct)
        {
            if (myproduct == null)
            {
                return BadRequest("Product can not be empty!");
            }
            var productexist = _db.products.Where(x => x.Id == myproduct.Id).FirstOrDefault();
            {
                return BadRequest("Product can not be found!");
            }
            
            var product = await _db.products.FirstOrDefaultAsync(x => x.Id == myproduct.Id);

            product.ExpirationDate = myproduct.ExpirationDate;
            product.ProductDescription = myproduct.ProductDescription;
            product.ProductName = myproduct.ProductName;
            product.ProductPrice = myproduct.ProductPrice;
            product.Manufacturer = myproduct.Manufacturer;
            product.DateUpdated = DateTime.Now;
            product.UpdatedBy = 2;


            _db.Update(product);



            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                var successmsg = "You have updated your product successfully";
                return Ok(successmsg);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
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
            var myproduct = _db.products.Where(x => x.Id == id).FirstOrDefault();

            if (myproduct == null)
            {
                var errormsg = "Product does not exist";
                return Ok(errormsg);
            }

            var product = await _db.products.FirstOrDefaultAsync(x => x.Id == id);

            product.IsDeleted = true;
            product.DateUpdated = DateTime.Now;

            _db.Update(product);



            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                var successmsg = "You have deleted the product successfully";
                return Ok(successmsg);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            return _db.products.Any(e => e.Id == id);
        }
    }
}
