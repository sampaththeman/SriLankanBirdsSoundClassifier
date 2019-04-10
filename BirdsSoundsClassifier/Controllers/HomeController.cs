using System.Web.Mvc;
using BirdsSoundsClassifier.Models;
using System.Linq;
using System.Net;

namespace BirdsSoundsClassifier.Controllers
{
    public class HomeController : Controller
    {

        private BirdsContext _context;

        public HomeController()
        {
            _context = new BirdsContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Redirection
        public ActionResult NewTaxonomy()
        {
            return View("~/Views/Home/NewTaxonomy.cshtml");
        }
        //Redirection
        public ActionResult EditTaxonomy()
        {
            return View("~/Views/Home/ViewTaxonCeation.cshtml");
        }

        //Create Taxonomy
        public ActionResult NewTaxonCreation(Taxonomy taxonomy)
        {
            string Msg = "";
            _context.Taxonomies.Add(taxonomy);
            _context.SaveChanges();
            Msg = "Succesfully Saved!";
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        //View Taxonomy
        public ActionResult ViewTaxonCeation()
        {
            return View(_context.Taxonomies.ToList());
        }

        //Details Taxonomy
        public ActionResult Details(int? id )
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Taxonomy tax = _context.Taxonomies.Find(id);
            if (tax == null)
                return HttpNotFound();
            return View(tax);
        }
        
        //Edit Taxonomy
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Taxonomy taxonomy = _context.Taxonomies.Find(id);
            if (taxonomy == null)
                return HttpNotFound();
            return View(taxonomy);
        }

        //Edit Taxonomy
        //public ActionResult Edit(Taxonomy taxonomy)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Entry(taxonomy).State = System.Data.Entity.EntityState.Modified;
        //            _context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch
        //    {
        //        return View();
        //    }
    

        //    return View(taxonomy);
        //}


        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
        }


    }
}
