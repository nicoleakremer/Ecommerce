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
    public class BillingController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: BILLINGs
        public ActionResult Index()
        {
            return View(db.BILLINGs.ToList());
        }

        // GET: BILLINGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILLING bILLING = db.BILLINGs.Find(id);
            if (bILLING == null)
            {
                return HttpNotFound();
            }
            return View(bILLING);
        }

        // GET: BILLINGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BILLINGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillingId,CustomerId,CardId,State,Street,City,Zip")] BILLING model)
        {
            if (ModelState.IsValid)
            {
                db.AddCreditCard(model.CardType, model.CardNumber, model.NameOnCard, model.ExpirationDate, model.SecurityCode);
                int custId = 0;
                int cardId = 0;
                custId = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First().CustomerId;
                cardId = db.CREDIT_CARD.Where(x => x.CardNumber == model.CardNumber).First().CardId;
                db.LinkCard(cardId, custId);
            }

            return View(model);
        }

        // GET: BILLINGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILLING bILLING = db.BILLINGs.Find(id);
            if (bILLING == null)
            {
                return HttpNotFound();
            }
            return View(bILLING);
        }

        // POST: BILLINGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillingId,CustomerId,CardId,State,Street,City,Zip")] BILLING bILLING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bILLING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bILLING);
        }

        // GET: BILLINGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILLING bILLING = db.BILLINGs.Find(id);
            if (bILLING == null)
            {
                return HttpNotFound();
            }
            return View(bILLING);
        }

        // POST: BILLINGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BILLING bILLING = db.BILLINGs.Find(id);
            db.BILLINGs.Remove(bILLING);
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
