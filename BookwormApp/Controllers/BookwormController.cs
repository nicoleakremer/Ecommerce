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
    public class BookwormController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: Bookworm
        public ActionResult Index()
        {
            var bOOKS = db.BOOKS.Include(b => b.PUBLISHER);
            return View(bOOKS.ToList());
        }

        // GET: Bookworm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOKS.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // GET: Bookworm/Create
        public ActionResult Create()
        {
            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name");
            return View();
        }

        // POST: Bookworm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,PublisherId,InventoryId,Title,Edition,Rating,Type,Description,ISBN,CopyRightDate")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.BOOKS.Add(bOOK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name", bOOK.PublisherId);
            return View(bOOK);
        }

        // GET: Bookworm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOKS.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name", bOOK.PublisherId);
            return View(bOOK);
        }

        // POST: Bookworm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,PublisherId,InventoryId,Title,Edition,Rating,Type,Description,ISBN,CopyRightDate")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PublisherId = new SelectList(db.PUBLISHERs, "PublisherId", "Name", bOOK.PublisherId);
            return View(bOOK);
        }

        // GET: Bookworm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOKS.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // POST: Bookworm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK bOOK = db.BOOKS.Find(id);
            db.BOOKS.Remove(bOOK);
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
