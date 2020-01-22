using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _db;
        public BooksController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book, int authorId)
        {
            _db.Books.Add(book);
            if(authorId != 0)
            {
                _db.AuthorBooks.Add(new AuthorBook() { AuthorId = authorId, BookId = book.BookId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Book book, int AuthorId)
        {
            if (AuthorId != 0)
            {
                _db.AuthorBooks.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId});
            }
            _db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisBook = _db.Books
                .Include(book => book.Authors)
                .ThenInclude(join => join.Author)
                .FirstOrDefault(book => book.BookId == id);
            return View(thisBook);
        }
    }
}