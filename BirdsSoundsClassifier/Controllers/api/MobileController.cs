using BirdsSoundsClassifier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BirdsSoundsClassifier.Controllers.api
{
    public class MobileController : ApiController
    {
        private BirdsContext _context;

        public MobileController()
        {
            _context = new BirdsContext();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/getBirdsLists")]
        public IHttpActionResult getBirdsListbyName([FromBody] string q)
        {
            var queryToList = (from ac in _context.Species
                               join ci in _context.Taxonomies on ac.BirdID equals ci.BirdID into gj
                               where ac.CommonName.Contains(q)
                               select new
                               {
                                   ac.BirdID,
                                   ac.CommonName,
                                   Taxo = gj
                               }).ToList();


            return Ok(queryToList);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/getBirdsListsALL")]
        public IHttpActionResult getBirdsListAll()
        {
            var queryToList = (from ac in _context.Species
                               join ci in _context.Taxonomies on ac.BirdID equals ci.BirdID into gj
                               
                               select new
                               {
                                   ac.BirdID,
                                   ac.CommonName,
                                   Taxo = gj
                               }).ToList();


            return Ok(queryToList);
        }

        [HttpPost]
        [Route("api/CreateUser")]
        [AllowAnonymous]
        public IdentityResult CreateUser(RegisterViewModel RegisterUsers)
        {
            try
            {
                var userStore = new UserStore<ApplicationUser>(new BirdsContext());
                var manager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser() { UserName = RegisterUsers.Email, Email = RegisterUsers.Email };
                IdentityResult result = manager.Create(user, RegisterUsers.Password);

                return result;

            }
            catch (Exception e)
            {
                IdentityResult result = null;
                return result;
            }

        }

        [HttpPost]

        [Route("api/LoginUser")]
        public async Task<IHttpActionResult> LoginUserAsync(LoginViewModel Login)
        {
            string url = "MM";
            RouteData route = new RouteData();
            AccountController controller = new AccountController();
            route.Values.Add("action", "LoginAsync"); // ActionName
            route.Values.Add("controller", "Account"); // Controller Name
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, controller);
            controller.ControllerContext = newContext;
            var msg =   await controller.LoginAsync(Login, url);

            return Ok(msg);
        }


        [HttpPost]
        [Route("api/UploadRecording")]
        public IHttpActionResult UploadRecording([FromBody]Sampling Sampling, HttpPostedFileBase file)
        {
            string Message = "";
            string fileName = "";
            // string extension = Path.GetExtension(file.FileName);

            try
            {

                if (ModelState.IsValid)
                {
                    Sampling.AudioFileName = file.FileName;
                    _context.Samplings.Add(Sampling);
                    _context.SaveChanges();
                    Message = "1";
                    return Ok(Message);
                }



                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Audio"), fileName);
                    file.SaveAs(path);

                }
                Message = "1";
                return Ok(Message);
            }
            catch (Exception ex)
            {
                Message = "0";
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/LoadDatatabe")]
        public IHttpActionResult LoadDataTable()
        {
            var queryToList = (from ac in _context.Users
                          
                               select new
                               {
                                   ac.UserName,
                                   ac.PhoneNumber,
                                   ac.Email,
                                   ac.Id
                               }).ToList();


            return Ok(queryToList);
        }

    }
}
