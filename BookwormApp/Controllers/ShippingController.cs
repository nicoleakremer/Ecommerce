using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookwormApp;
using System.Threading.Tasks;

namespace BookwormApp.Controllers
{
    public class ShippingController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: Shipping
        public ActionResult Index()
        {
            CUSTOMER customer = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First();
            var Shipment = from ship in db.SHIPPINGs
                           where ship.CustomerId == customer.CustomerId
                           select ship;

            return View(Shipment.ToList());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Shipment(SHIPPING model)
        {
            if (ModelState.IsValid)
            {
                db.AddShipping(model.CustomerId, model.State, model.Street, model.City, model.Zip);
                int custId = 0;
                int shippingId = 0;
                custId = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First().CustomerId;
               shippingId = db.SHIPPINGs.Where(x => x.CustomerId == model.CustomerId).First().ShippingId;
                db.LinkShipping(shippingId, custId);

            }


            return View(model);
        }


        // GET: Shipping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHIPPING sHIPPING = db.SHIPPINGs.Find(id);
            if (sHIPPING == null)
            {
                return HttpNotFound();
            }
            return View(sHIPPING);
        }

        // GET: Shipping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingId,CustomerId,State,Street,City,Zip")] SHIPPING model)
        {
            if (ModelState.IsValid)
            {
                db.AddShipping(model.CustomerId, model.State, model.Street, model.City, model.Zip);
                int custId = 0;
                int shippingId = 0;
                custId = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First().CustomerId;
                shippingId = db.SHIPPINGs.Where(x => x.CustomerId == model.CustomerId).First().ShippingId;
                db.LinkShipping(shippingId, custId);
            }

            return RedirectToAction("Index");
        }

        // GET: Shipping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHIPPING sHIPPING = db.SHIPPINGs.Find(id);
            if (sHIPPING == null)
            {
                return HttpNotFound();
            }
            return View(sHIPPING);
        }

        // POST: Shipping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingId,CustomerId,State,Street,City,Zip")] SHIPPING sHIPPING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sHIPPING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sHIPPING);
        }

        // GET: Shipping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHIPPING sHIPPING = db.SHIPPINGs.Find(id);
            if (sHIPPING == null)
            {
                return HttpNotFound();
            }
            return View(sHIPPING);
        }

        // POST: Shipping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SHIPPING sHIPPING = db.SHIPPINGs.Find(id);
            db.SHIPPINGs.Remove(sHIPPING);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
