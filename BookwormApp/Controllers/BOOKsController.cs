using BookwormApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookwormApp.Controllers
{
    public class BooksController : Controller
    {
        DataEntities db = new DataEntities();
        // GET: Default
        public ActionResult Index()
        {
            //Book book = new Book();
            List<Book> books = new List<Book>();
            var BookList = db.BOOKS.ToList();

            foreach (var item in BookList)
            {
                //stuff
                Book book = new Book();
                AUTHOR Author = null;
                try
                {
                    BOOK_AUTHOR BookAuthor = db.BOOK_AUTHOR.Where(x => x.BookId == item.BookId).First();
                    Author = db.AUTHORs.Where(x => x.AuthorId == BookAuthor.AuthorId).First();
                    book.RetailPrice = db.INVENTORies.Where(x => x.InventoryId == item.InventoryId).First().RetailPrice;
                }
                catch (Exception e)
                {
                    book.RetailPrice = 24.99M;
                }
                book.BookId = item.BookId;
                book.Title = item.Title;
                book.Edition = item.Edition;
                book.Rating = item.Rating;
                book.Type = item.Type;
                book.Description = item.Description;
                book.ISBN = item.ISBN;
                book.CopyRightDate = item.CopyRightDate;
                if (Author == null)
                {
                    book.FirstName = "Author Unknown";
                    book.LastName = "Author Unknown";
                    book.Biography = "No Biography Available";
                }
                else
                {
                    book.FirstName = Author.FirstName;
                    book.LastName = Author.LastName;
                    book.Biography = Author.Biography;
                }

                books.Add(book);
            }
            ViewBag.NoStock = TempData["NoStock"];
            return View(books.ToList());
        }
       
        public ActionResult AddToCart(int? id)
        {
            int cust = 0;
            cust = db.CUSTOMERs.Where(x => x.Email == User.Identity.Name).First().CustomerId;
            CART cart = db.CARTs.Where(x => x.CustomerId == cust).First();
           if( db.AddBookToCart(cart.CartId, id, 1 ) == -1){
               TempData["NoStock"] = "<div class=\"alert alert-warning alert-dismissible\" role=\"alert\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button><strong>Warning!</strong> This book is out of stock!</div>";
               return RedirectToAction("Index", "BOOKs");
           }
           else
           {
               return RedirectToAction("Index", "CARTs", null);
           }
        }
    
    }

    
}