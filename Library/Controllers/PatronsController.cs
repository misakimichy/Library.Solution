using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        private readonly LibraryContext _db;
        public PatronsController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Patrons.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patron patron)
        {
            _db.Patrons.Add(patron);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisPatron = _db.Patrons
                .Include(patron => patron.Copies)
                .ThenInclude(join => join.Copy)
                .FirstOrDefault(patron => patron.PatronId == id);
            return View(thisPatron);
        }

        public ActionResult Edit(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            return View(thisPatron);
        }

        [HttpPost]
        public ActionResult Edit(Patron patron)
        {
            _db.Entry(patron).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddCopy(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyId");
            return View(thisPatron);
        }

        [HttpPost]
        public ActionResult AddCopy(Patron patron, int CopyId)
        {
            PatronCopy join = _db.PatronCopy.FirstOrDefault(PatronCopy => PatronCopy.CopyId == CopyId && PatronCopy.PatronId == patron.PatronId);
            if(CopyId != 0 && join == null)
            {
                _db.PatronCopy.Add(new PatronCopy(){ CopyId = CopyId, PatronId= patron.PatronId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            return View(thisPatron);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            _db.Patrons.Remove(thisPatron);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}