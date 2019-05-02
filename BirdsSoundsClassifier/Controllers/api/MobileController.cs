using BirdsSoundsClassifier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace BirdsSoundsClassifier.Controllers.api
{
    [EnableCors("*", "*", "*")]
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
        public IHttpActionResult getBirdsListbyName([FromBody] SendModel q)
        {
            var queryToList = (from ac in _context.Species
                               join ci in _context.Taxonomies on ac.BirdID equals ci.BirdID into gj
                               where ac.CommonName.Contains(q.q)
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
        public async Task<IHttpActionResult> CreateUserAsync(RegisterViewModel RegisterUsers)
        {

            RouteData route = new RouteData();
            AccountController controller = new AccountController();
            route.Values.Add("action", "RegisterAsync"); // ActionName
            route.Values.Add("controller", "Account"); // Controller Name
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, controller);
            controller.ControllerContext = newContext;
            RegisterUsers.RoleName = "UserRole";
            var msg = await controller.RegisterAsync(RegisterUsers);

            return Ok(msg);


            //try
            //{
            //    var userStore = new UserStore<ApplicationUser>(new BirdsContext());
            //    var manager = new UserManager<ApplicationUser>(userStore);
            //    var user = new ApplicationUser() { UserName = RegisterUsers.Email, Email = RegisterUsers.Email};
            //    IdentityResult result = manager.Create(user, RegisterUsers.Password);
            //    result =  UserManager.AddToRoleAsync(user.Id, model.RoleName);

            //    return result;

            //}
            //catch (Exception e)
            //{
            //    IdentityResult result = null;
            //    return result;
            //}

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


        [HttpPost]
        [Route("api/upload")]
        public HttpResponseMessage uploadImage()
        {
            var request = HttpContext.Current.Request;
            string results="";
            if (Request.Content.IsMimeMultipartContent())
            {
                if (request.Files.Count > 0)
                {
                    var postedFile = request.Files.Get("file");
                    string root = HttpContext.Current.Server.MapPath("~/Uploads/Audio");
                    root = root + "/" + postedFile.FileName;
                    postedFile.SaveAs(root);
                    WaveFileReader wf = new WaveFileReader(root);
                    string minutes = wf.TotalTime.TotalMinutes.ToString();



                    Sampling sampling = new Sampling();
                    sampling.AudioFileName = postedFile.FileName;
                    sampling.CreatedAt = DateTime.Now;
                    sampling.ObservationType = "Field Recoding";
                    sampling.ObservationDuaration = minutes;
                    _context.Samplings.Add(sampling);
                    _context.SaveChanges();

                   // var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/classifyPost");
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://40.117.35.70:5000/classifyPost");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            url = root,
                        });

                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        results = result.ToString();
                        
                    }
                }
            }
            else
            {
          

                return Request.CreateResponse(HttpStatusCode.Found, new
                {
                    error = true,
                    status = "Not Created Something wrong",
                    prediction = "Error"
                });
            }

            var data = (from c in _context.Species
                       join ci in _context.Taxonomies on c.BirdID equals ci.BirdID into gj
                       where c.CommonName.Contains(results.ToString())
                       select new
                       {
                           c.BirdID,
                           c.CommonName,
                           gj = gj
                       }).ToList();

            return Request.CreateResponse(HttpStatusCode.Found, new
            {
                error = false,
                status = "Success",

                prediction = data
        });

          
        }

        [HttpPost]
        [Route("api/test")]
        public HttpResponseMessage test()
        {
            string results = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://40.117.35.70:5000/classifyPost");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    url = HttpContext.Current.Server.MapPath("~/Uploads/Audio/sr-1.wav"),
                });

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                results = result.ToString();
            }

            var data = (from c in _context.Species
                        join ci in _context.Taxonomies on c.BirdID equals ci.BirdID into gj
                        where c.CommonName.Contains(results.ToString())
                        select new
                        {
                            c.BirdID,
                            c.CommonName,
                            gj = gj
                        }).ToList();


            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                error = false,
                status = "Success",

                prediction = data
            });
        }

    }

    public class  SendModel
    {
        public string q { get; set; }
    }

}
