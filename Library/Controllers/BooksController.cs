using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(LibraryContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userItems = _db.Books.Where(entry => entry.User.Id == currentUser.Id);
            return View(userItems);
        }

        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Book book, int authorId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            book.User = currentUser;
            _db.Books.Add(book);
            if(authorId != 0)
            {
                _db.AuthorBooks.Add(new AuthorBook() { AuthorId = authorId, BookId = book.BookId});
            }
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

        public ActionResult Edit(int id)
        {
            var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
            ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
            return View(thisBook);
        }

        [HttpPost]
        public ActionResult Edit(Book book, int AuthorId)
        {
            if (AuthorId != 0)
            {
                _db.AuthorBooks.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId});
            }
            _db.Entry(book).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
            return View(thisBook);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
            _db.Books.Remove(thisBook);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddAuthor(int id)
        {
            var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
            ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
            return View(thisBook);
        }

        [HttpPost]
        public ActionResult AddAuthor(Book book, int AuthorId)
        {
            AuthorBook join = _db.AuthorBooks.FirstOrDefault(authorBook => authorBook.AuthorId == AuthorId && authorBook.BookId == book.BookId);
            if(AuthorId != 0 && join == null)
            {
                _db.AuthorBooks.Add(new AuthorBook(){ AuthorId = AuthorId, BookId = book.BookId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}