using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}