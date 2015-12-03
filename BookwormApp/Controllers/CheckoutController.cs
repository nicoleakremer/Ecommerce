using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookwormApp.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        DataEntities db = new DataEntities();
        const string PromoCode = "FREE";
        // GET: Checkout
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var shipping = new SHIPPING();
            TryUpdateModel(shipping);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(shipping);
                }
                else
                {
                    shipping.CustomerId = User.Identity.Name;
                    shipping.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(shipping);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(shipping);

                    return RedirectToAction("Complete",
                        new { id = shipping.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}