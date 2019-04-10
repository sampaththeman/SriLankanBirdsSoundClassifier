using BirdsSoundsClassifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BirdsSoundsClassifier.Controllers
{
    public class SamplingController : Controller
    {

        private BirdsContext _context;

        public SamplingController()
        {
            _context = new BirdsContext();
        }

        // GET: Sampling
        public ActionResult Index()
        {
            return View(_context.Samplings.ToList());
        }

        // GET: Sampling/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Sampling tax = _context.Samplings.Find(id);
            if (tax == null)
                return HttpNotFound();
            return View(tax);
        }

        // GET: Sampling/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sampling/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sampling/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Sampling tax = _context.Samplings.Find(id);
            if (tax == null)
                return HttpNotFound();
            return View(tax);
        }

        // POST: Sampling/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sampling/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sampling/Delete/5
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
