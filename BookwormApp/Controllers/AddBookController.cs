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
    public class AddBookController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: /AddBook/
        public ActionResult Index()
        {
            var books = db.BOOKS.Include(b => b.PUBLISHER);
            return View(books.ToList());
        }

        // GET: /AddBook/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK book = db.BOOKS.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /AddBook/Create
        public ActionResult Create()
        {
            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name");
            return View();
        }

        // POST: /AddBook/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BookId,PublisherId,InventoryId,Title,Edition,Rating,Type,Description,ISBN,CopyRightDate")] BOOK book)
        {
            if (ModelState.IsValid)
            {
                db.BOOKS.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name", book.PublisherId);
            return View(book);
        }

        // GET: /AddBook/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK book = db.BOOKS.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name", book.PublisherId);
            return View(book);
        }

        // POST: /AddBook/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BookId,PublisherId,InventoryId,Title,Edition,Rating,Type,Description,ISBN,CopyRightDate")] BOOK book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name", book.PublisherId);
            return View(book);
        }

        // GET: /AddBook/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK book = db.BOOKS.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /AddBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK book = db.BOOKS.Find(id);
            db.BOOKS.Remove(book);
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
