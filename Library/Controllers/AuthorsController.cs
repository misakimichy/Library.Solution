using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _db;
        public AuthorsController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Authors.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisAuthor = _db.Authors
                .Include(author => author.Books)
                .ThenInclude(join => join.Book)
                .FirstOrDefault(author => author.AuthorId == id);
            return View(thisAuthor);
        }

        public ActionResult Edit(int id)
        {
            var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id );
            return View(thisAuthor);
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            _db.Entry(author).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}