using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    public class CopiesController : Controller
    {
        private readonly LibraryContext _db;
        public CopiesController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Copies.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Copy copy, int patronId)
        {
            _db.Copies.Add(copy);
            if(patronId != 0)
            {
                _db.PatronCopy.Add(new PatronCopy(){ PatronId = patronId, CopyId = copy.CopyId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}