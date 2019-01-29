using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.SqlClient;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BugReporter_v2.DAL;
using BugReporter_v2.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


namespace BugReporter_v2.Controllers
{
    [Authorize]
    public class BugController : Controller
    {
        private BugReporter_v2Entities db = new BugReporter_v2Entities();

        //
        // GET: /Bug/
        [Compress]
        public ActionResult Index()
        {
            List<Bug> bugs = BugDAL.GetAllBugs();
            List<BugViewModel> allBugs = new List<BugViewModel>();
            foreach (var item in bugs)
            {
                string owner = item.UserId == null ? "None" : item.UserProfile.UserName;
                BugViewModel bug = new BugViewModel()
                {
                    ProjectName = item.Project.ProjectName,
                    Desctiption=item.BugDescription,
                    Owner=owner,
                    Priority = item.Priority,
                    Status= item.Status,
                    BugId= item.BugId,
                    ProjectId = item.Project.ProjectId,
                    DateOfFirstSubmit = item.DateOfFirstSubmit
                };
                allBugs.Add(bug);
            }
            if (User.Identity.IsAuthenticated)
            {
                UserDAL.AddActivity(User.Identity.Name);
            }

            return View(allBugs);
        }

        public ActionResult AllProjects()
        {
            var projects = ProjectDAL.GetAllProject();
            return Json(db.Projects.Select(x => x.ProjectName).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProjects()
        {

            return Json(db.Projects.Select(x => new { x.ProjectName, x.ProjectId }), JsonRequestBehavior.AllowGet);
        }



        //
        // GET: /Bug/Create

        public ActionResult Create()
        {

            //ModelState.AddModelError("", "What a terrible name!");
            ViewBag.Projects = new SelectList(db.Projects.OrderBy(x => x.ProjectName).Select(x => x), "ProjectId", "ProjectName");
            if (User.Identity.IsAuthenticated)
            {
                UserDAL.AddActivity(User.Identity.Name);
            }
            return View();
        }


        //
        // POST: /Bug/SubmitNewBug/5
        [HttpPost]
        public ActionResult Create(CreateBugModel model)
        {
            if (ModelState.IsValid)
            {
                BugReporter_v2.DAL.BugDAL.SubmitBugToProject(model.ProjectID, User.Identity.Name, "New", model.Desctiption, model.Priority);
                var bug = BugReporter_v2.DAL.BugDAL.GetLastAddedBug().First();
                //BugReporter_v2.DAL.UserProfile user = User.Identity.Name;
                LogDAL.AddLogForBug((string)User.Identity.Name, bug.BugId,"Add");
                UserDAL.AddActivity(User.Identity.Name);

                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                // If we got this far, something failed, redisplay form
                ViewBag.error = "error";
                UserDAL.AddActivity(User.Identity.Name);
                ModelState.AddModelError("", "Error please try again");
                ViewBag.Projects = new SelectList(db.Projects.OrderBy(x => x.ProjectName).Select(x => x), "ProjectId", "ProjectName",model.ProjectID);
                return View(model);
            }
        }
        //
        // POST: /Bug/Edit/5
        [HttpPost]
        public ActionResult Edit(EditBugModel model, int BugID)
        {
            if (ModelState.IsValid)
            {
                BugReporter_v2.DAL.BugDAL.EditBug(model.ProjectID, model.Owner, model.Status,
                    model.Desctiption, model.Priority, BugID);
                var bug = BugReporter_v2.DAL.BugDAL.GetBugByID(BugID);
                if(String.IsNullOrEmpty(User.Identity.Name))
                {
                    return HttpNotFound();
                }
                LogDAL.AddLogForBug(User.Identity.Name, bug.BugId,"Edit");
                UserDAL.AddActivity(User.Identity.Name);
                return RedirectToAction("Index", "Bug");
            }
            else
            {
                // If we got this far, something failed, redisplay form

                UserDAL.AddActivity(User.Identity.Name);
                ModelState.AddModelError("", "Error please try again");
                ViewBag.Projects = new SelectList(db.Projects.OrderBy(x => x.ProjectName).Select(x => x), "ProjectId", "ProjectName",model.ProjectID);
                ViewBag.Users = new SelectList(db.UserProfiles.OrderBy(x => x.UserName).Select(x => x), "UserId", "UserName",model.Owner);
                ViewBag.BugID = BugID;
                return View(model);
             
            }

        }
        //
        // GET: /Bug/Edit/5

        public ActionResult Edit(string Owner = "", int BugId=0 )
        {
            Bug bug = BugDAL.GetBugByID(BugId);
            if (bug == null || BugId<=0 || Owner.Equals(""))
            {
                return HttpNotFound();
            }
            int owner;
            if (Owner.Equals("None")) owner = 1;
            else owner = bug.UserProfile.UserId;
            EditBugModel editBug = new EditBugModel()
            {
                Owner = owner,
                Desctiption = bug.BugDescription,
                ProjectName = bug.Project.ProjectName,
                ProjectID = bug.Project.ProjectId,
                Priority = bug.Priority,
                Status = bug.Status
            };
            ViewBag.Projects = new SelectList(db.Projects.OrderBy(x => x.ProjectName).Select(x => x), "ProjectId", "ProjectName");
            ViewBag.Users = new SelectList(db.UserProfiles.OrderBy(x => x.UserName).Select(x => x), "UserId", "UserName");
            ViewBag.BugID = BugId;
            return View(editBug);
        }

        [Compress]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int BugId=0)
        {
            var bug = BugReporter_v2.DAL.BugDAL.GetBugByID(BugId);
            RouteValueDictionary routeValues;

            if (bug == null)
            {

                routeValues = this.GridRouteValues();

                return RedirectToAction("Index", routeValues);
            }
            LogDAL.AddLogForBug(User.Identity.Name, bug.BugId, "Delete");
            UserDAL.AddActivity(User.Identity.Name);
            BugDAL.DeleteBug(BugId);
            routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }


        public ActionResult ProjectBugs(int ProjectId = 0)
        {
            List<BugViewModel> bugsForProject = new List<BugViewModel>();
            if (ProjectId==0)
            {
                ViewBag.Projects = new SelectList(db.Projects.OrderBy(x => x.ProjectName)
                    .Select(x => x), "ProjectId", "ProjectName");
                return View(bugsForProject);
            }
            ViewBag.Projects = new SelectList(db.Projects.OrderBy(x => x.ProjectName)
                    .Select(x => x), "ProjectId", "ProjectName",ProjectId);
            var bugs = BugDAL.SelectAllBugsFromProject(ProjectId);
            foreach (var item in bugs)
	        {
                string owner = item.UserId == null ? "None" : item.UserProfile.UserName;
		         BugViewModel bug = new BugViewModel()
                {
                    Owner = owner,
                    Desctiption = "",
                    Priority = item.Priority,
                    Status = item.Status,
                    BugId=item.BugId,
                    ProjectId=ProjectId,
                    ProjectName=item.Project.ProjectName,
                    DateOfFirstSubmit=item.DateOfFirstSubmit,
                    
                };
                bugsForProject.Add(bug);
	        }

            return View(bugsForProject);
        }

        public class CompressAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                var encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
                if (string.IsNullOrEmpty(encodingsAccepted)) return;

                encodingsAccepted = encodingsAccepted.ToLowerInvariant();
                var response = filterContext.HttpContext.Response;

                if (encodingsAccepted.Contains("deflate"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
                else if (encodingsAccepted.Contains("gzip"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}