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
    public class BOOK_CARTController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: BOOK_CART
        public ActionResult Index()
        {
            var bOOK_CART = db.BOOK_CART.Include(b => b.CART).Include(b => b.BOOK);
            return View(bOOK_CART.ToList());
        }

        // GET: BOOK_CART/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_CART bOOK_CART = db.BOOK_CART.Find(id);
            if (bOOK_CART == null)
            {
                return HttpNotFound();
            }
            return View(bOOK_CART);
        }

        // GET: BOOK_CART/Create
        public ActionResult Create()
        {
            ViewBag.CartId = new SelectList(db.CARTs, "CartId", "CartId");
            ViewBag.BookId = new SelectList(db.BOOKS, "BookId", "Title");
            return View();
        }

        // POST: BOOK_CART/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,CartId,Quantity")] BOOK_CART bOOK_CART)
        {
            if (ModelState.IsValid)
            {
                db.BOOK_CART.Add(bOOK_CART);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartId = new SelectList(db.CARTs, "CartId", "CartId", bOOK_CART.CartId);
            ViewBag.BookId = new SelectList(db.BOOKS, "BookId", "Title", bOOK_CART.BookId);
            return View(bOOK_CART);
        }

        // GET: BOOK_CART/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_CART bOOK_CART = db.BOOK_CART.Find(id);
            if (bOOK_CART == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartId = new SelectList(db.CARTs, "CartId", "CartId", bOOK_CART.CartId);
            ViewBag.BookId = new SelectList(db.BOOKS, "BookId", "Title", bOOK_CART.BookId);
            return View(bOOK_CART);
        }

        // POST: BOOK_CART/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,CartId,Quantity")] BOOK_CART bOOK_CART)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK_CART).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartId = new SelectList(db.CARTs, "CartId", "CartId", bOOK_CART.CartId);
            ViewBag.BookId = new SelectList(db.BOOKS, "BookId", "Title", bOOK_CART.BookId);
            return View(bOOK_CART);
        }

        // GET: BOOK_CART/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_CART bOOK_CART = db.BOOK_CART.Find(id);
            if (bOOK_CART == null)
            {
                return HttpNotFound();
            }
            return View(bOOK_CART);
        }

        // POST: BOOK_CART/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK_CART bOOK_CART = db.BOOK_CART.Find(id);
            db.BOOK_CART.Remove(bOOK_CART);
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
