using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookwormApp;

namespace BookwormApp.Controllers
{
    public class INVOICEsController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: INVOICEs
        public ActionResult Index()
        {
            CUSTOMER customer = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First();
            INVOICE invoice = db.INVOICEs.Where(x => x.CustomerId == customer.CustomerId).First();
            //CART cart = db.CARTs.Where(x => x.CustomerId == customer.CustomerId).First();
            //SHIPPING shipping = db.SHIPPINGs.Where(x => x.CustomerId == cart.CustomerId).First();
            //CREDIT_CARD card = db.CREDIT_CARD.Where(x => x.CardId == s);



            var iNVOICEs = db.INVOICEs.Include(i => i.CUSTOMER);
            return View(iNVOICEs.ToList());
        }

        // GET: INVOICEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVOICE iNVOICE = db.INVOICEs.Find(id);
            if (iNVOICE == null)
            {
                return HttpNotFound();
            }
            return View(iNVOICE);
        }

        // GET: INVOICEs/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email");
            return View();
        }

        // POST: INVOICEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,CustomerId,CartId,BillingId,ShippingId,TotalCost,DateOrder")] INVOICE iNVOICE)
        {
            if (ModelState.IsValid)
            {
                db.INVOICEs.Add(iNVOICE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email", iNVOICE.CustomerId);
            return View(iNVOICE);
        }

        // GET: INVOICEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVOICE iNVOICE = db.INVOICEs.Find(id);
            if (iNVOICE == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email", iNVOICE.CustomerId);
            return View(iNVOICE);
        }

        // POST: INVOICEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,CustomerId,CartId,BillingId,ShippingId,TotalCost,DateOrder")] INVOICE iNVOICE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNVOICE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email", iNVOICE.CustomerId);
            return View(iNVOICE);
        }

        // GET: INVOICEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVOICE iNVOICE = db.INVOICEs.Find(id);
            if (iNVOICE == null)
            {
                return HttpNotFound();
            }
            return View(iNVOICE);
        }

        // POST: INVOICEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INVOICE iNVOICE = db.INVOICEs.Find(id);
            db.INVOICEs.Remove(iNVOICE);
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
