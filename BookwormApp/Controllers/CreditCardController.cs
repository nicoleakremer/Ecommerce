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
using BookwormApp.Models;

namespace BookwormApp.Controllers
{
    public class CreditCardController : Controller
    {
        private DataEntities db = new DataEntities();

        // GET: CreditCard
        public ActionResult Index()
        {
            CUSTOMER customer = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First();
            var cards = from card in db.CREDIT_CARD
                        where card.CustomerId == customer.CustomerId
                        select card;

            return View(cards.ToList());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Payment(CREDIT_CARD model)
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

        // GET: CreditCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CREDIT_CARD cREDIT_CARD = db.CREDIT_CARD.Find(id);
            if (cREDIT_CARD == null)
            {
                return HttpNotFound();
            }
            return View(cREDIT_CARD);
        }

        // GET: CreditCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardId,CardType,CardNumber,NameOnCard,ExpirationDate,SecurityCode")] CREDIT_CARD model)
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

            return RedirectToAction("Index");
        }

        // GET: CreditCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CREDIT_CARD cREDIT_CARD = db.CREDIT_CARD.Find(id);
            if (cREDIT_CARD == null)
            {
                return HttpNotFound();
            }
            return View(cREDIT_CARD);
        }

        // POST: CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardId,CardType,CardNumber,NameOnCard,ExpirationDate,SecurityCode")] CREDIT_CARD cREDIT_CARD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cREDIT_CARD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cREDIT_CARD);
        }

        // GET: CreditCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CREDIT_CARD cREDIT_CARD = db.CREDIT_CARD.Find(id);
            if (cREDIT_CARD == null)
            {
                return HttpNotFound();
            }
            return View(cREDIT_CARD);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CREDIT_CARD cREDIT_CARD = db.CREDIT_CARD.Find(id);
            db.CREDIT_CARD.Remove(cREDIT_CARD);
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
