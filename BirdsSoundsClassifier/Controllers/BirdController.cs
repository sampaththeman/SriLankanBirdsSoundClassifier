using BirdsSoundsClassifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BirdsSoundsClassifier.Controllers
{
    public class BirdController : Controller
    {

        private BirdsContext _context;

        public BirdController()
        {
            _context = new BirdsContext();
        }



        // GET: Bird
        public ActionResult Index()
        {
            return View(_context.Species.ToList());
        }

        // GET: Bird/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Species tax = _context.Species.Find(id);
            if (tax == null)
                return HttpNotFound();
            return View(tax);
        }

        // GET: Bird/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bird/Create
        [HttpPost]
        public ActionResult Create(Species collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Species.Add(collection);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(collection);

            }
            catch
            {
                return View();
            }
        }

        // GET: Bird/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Species taxonomy = _context.Species.Find(id);
            if (taxonomy == null)
                return HttpNotFound();
            return View(taxonomy);
        }

        // POST: Bird/Edit/5
        [HttpPost]
        public ActionResult Edit(Species collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(collection).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Bird/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bird/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
