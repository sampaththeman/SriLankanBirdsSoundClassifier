using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BirdsSoundsClassifier.Models;

namespace BirdsSoundsClassifier.Controllers
{
    public class TaxonomyController : Controller
    {
        private BirdsContext _context;

        public TaxonomyController()
        {
            _context = new BirdsContext();
        }


        // GET: Taxonomy
        public ActionResult Index()
        {

            return View(_context.Taxonomies.ToList());
        }

        // GET: Taxonomy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Taxonomy tax = _context.Taxonomies.Find(id);
            if (tax == null)
                return HttpNotFound();
            return View(tax);
        }

        // GET: Taxonomy/Create
        public ActionResult Create()
        {
            ViewBag.Birds = new SelectList(_context.Species.ToList(), "BirdID", "CommonName", "0");
            return View();
        }

        // POST: Taxonomy/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "BirdID")]  Taxonomy model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Taxonomies.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: Taxonomy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Taxonomy tax = _context.Taxonomies.Find(id);
            if (tax == null)
                return HttpNotFound();
            return View(tax);
        }

        // POST: Taxonomy/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "BirdID")]  Taxonomy model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Taxonomy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Taxonomy/Delete/5
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
