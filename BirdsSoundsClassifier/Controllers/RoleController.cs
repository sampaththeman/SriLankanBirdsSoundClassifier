using BirdsSoundsClassifier.App_Start;
using BirdsSoundsClassifier.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BirdsSoundsClassifier.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {

        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            AppRoleManager = roleManager;
        }

        public ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }

            private set
            {
                _roleManager = value;
            }

        }

        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModels> list = new List<RoleViewModels>();
            foreach (var role in AppRoleManager.Roles)
                list.Add(new RoleViewModels(role));
            return View(list);
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModels model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await AppRoleManager.CreateAsync(role);
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);
            return View(new RoleViewModels(role));
            //  return View();
        }


        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModels models)
        {
            var role = new ApplicationRole() { Id = models.Id, Name = models.Name };
            await AppRoleManager.UpdateAsync(role);
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Details(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);
            return View(new RoleViewModels(role));
            //return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);
            return View(new RoleViewModels(role));
            // return View();
        }


        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);
            await AppRoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}