using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookwormApp;
using BookwormApp.Models;

namespace BookwormApp.Controllers
{
    public class CARTsController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: CARTs
        public ActionResult Index()
        {
            CUSTOMER customer = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First();
            CART cart = db.CARTs.Where(x => x.CustomerId == customer.CustomerId).First();
            var list = db.BOOK_CART.Where(x => x.CartId == cart.CartId);
            List<Cart> CartList = new List<Cart>();
            foreach (var item in list)
            {
                Cart Shopping = new Cart();
                BOOK Book = db.BOOKS.Where(x => x.BookId == item.BookId).First();
                Shopping.RetailPrice = db.INVENTORies.Where(x => x.InventoryId == Book.InventoryId).First().RetailPrice;
                Shopping.Title = Book.Title;
                Shopping.Edition = Book.Edition;
                Shopping.Rating = Book.Rating;
                Shopping.Type = Book.Type;
                Shopping.Description = Book.Description;
                Shopping.ISBN = Book.ISBN;
                Shopping.CopyRightDate = Book.CopyRightDate;
                Shopping.Quantity = item.Quantity;

                CartList.Add(Shopping);

            }
            var cARTs = db.CARTs.Include(c => c.CUSTOMER);
            return View(CartList.ToList());
        }

        // GET: CARTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART cART = db.CARTs.Find(id);
            if (cART == null)
            {
                return HttpNotFound();
            }
            return View(cART);
        }

        // GET: CARTs/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email");
            return View();
        }

        // POST: CARTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartId,CustomerId,IsActive")] CART cART)
        {
            if (ModelState.IsValid)
            {
                db.CARTs.Add(cART);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email", cART.CustomerId);
            return View(cART);
        }

        // GET: CARTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART cART = db.CARTs.Find(id);
            if (cART == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email", cART.CustomerId);
            return View(cART);
        }

        // POST: CARTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CartId,CustomerId,IsActive")] CART cART)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cART).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CUSTOMERs, "CustomerId", "Email", cART.CustomerId);
            return View(cART);
        }

        // GET: CARTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART cART = db.CARTs.Find(id);
            if (cART == null)
            {
                return HttpNotFound();
            }
            return View(cART);
        }

        // POST: CARTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CART cART = db.CARTs.Find(id);
            db.CARTs.Remove(cART);
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
