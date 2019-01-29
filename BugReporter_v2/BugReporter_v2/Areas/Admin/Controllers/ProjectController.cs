using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugReporter_v2.DAL;
using BugReporter_v2.Areas.Admin.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.IO.Compression;
using System.Web.Routing;

namespace BugReporter_v2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProjectController : Controller
    {
        private BugReporter_v2Entities db = new BugReporter_v2Entities();

        //
        // GET: /Admin/Project/
        


        [Compress]
        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            List<Project> allProjects = ProjectDAL.AllProject();
            List<ProjectViewModels> projects = new List<ProjectViewModels>();
            foreach (var project in allProjects)
            {
                ProjectViewModels item = new ProjectViewModels()
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    Description = project.ProjectDescription
                };
                projects.Add(item);
            }

            return View(projects);
        }
        // GET: /Admin/Project/Create/5
        [Compress]
        public ActionResult Create()
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            return View();
        }
        // Post: /Admin/Project/Create/5
        [Compress]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ProjectViewModels project)
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            bool projectIsExist = ProjectDAL.IsProjectExist(project.ProjectName, project.Description);
            if (ModelState.IsValid && !projectIsExist)
            {

                ProjectDAL.AddProject(project.ProjectName, project.Description);
 

                return RedirectToAction("Index", "Project");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Project already exist");
            return View(project);
        }

        // GET: /Admin/Project/Update/5

        public ActionResult Update(int ProjectId = 0, string ProjectName = "")
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            Project project = ProjectDAL.FindProject(ProjectId);
            if (project == null)
            {
                return HttpNotFound();
            }
            ProjectViewModels Project = new ProjectViewModels()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Description = project.ProjectDescription
            };
            return View(Project);
        }


        [Compress]
        [HttpPost]
        public ActionResult Update(ProjectViewModels project)
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            bool isProjectExist = ProjectDAL.IsProjectExist(project.ProjectName, project.Description);
            if (ModelState.IsValid && !isProjectExist)
            {
             
                ProjectDAL.EditProject(project.ProjectId,project.ProjectName,project.Description);


                return RedirectToAction("Index","Project");
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("","Project already exist");
            return View(project);
        }
        [Compress]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy(int ProjectId)
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            var project = BugReporter_v2.DAL.ProjectDAL.FindProject(ProjectId);
            RouteValueDictionary routeValues;

            if (project == null)
            {

                routeValues = this.GridRouteValues();

                return RedirectToAction("Index", routeValues);
            }
            ProjectDAL.DeleteProject(ProjectId);
            routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }

        #region default scafolded action
        //[Compress]
        //public ActionResult Index()
        //{
        //    return View(ProjectDAL.AllProject());
        //}
        //public ActionResult GetAllProjects([DataSourceRequest]DataSourceRequest request)
        //{
        //    return Json(ProjectDAL.AllProject().ToDataSourceResult(request));
        //}

        ////
        //// GET: /Admin/Project/Details/5
        //[Compress]
        //public ActionResult Details(int id = 0)
        //{
        //    Project project = ProjectDAL.FindProject(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ProjectViewModels Project = new ProjectViewModels()
        //    {
        //        ProjectId = project.ProjectId,
        //        ProjectName = project.ProjectName,
        //        Description = project.ProjectDescription
        //    };
        //    return View(Project);
        //}

        ////
        //// GET: /Admin/Project/Create
        ////[Compress]
        ////public ActionResult Create()
        ////{
        ////    return View();
        ////}

        //////
        ////// POST: /Admin/Project/Create

        ////[HttpPost]
        ////public ActionResult Create(ProjectViewModels project)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        ProjectDAL.AddProject(project.ProjectName, project.Description);
        ////        return RedirectToAction("Index");
        ////    }

        ////    return View(project);
        ////}

        ////
        //// GET: /Admin/Project/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Project project = ProjectDAL.FindProject(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ProjectViewModels Project = new ProjectViewModels()
        //    {
        //        ProjectId = project.ProjectId,
        //        ProjectName = project.ProjectName,
        //        Description = project.ProjectDescription
        //    };
        //    return View(Project);
        //}

        ////
        //// POST: /Admin/Project/Edit/5

        //[HttpPost]
        //public ActionResult Edit(ProjectViewModels project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ProjectDAL.EditProject(project.ProjectId,project.ProjectName,project.Description);
        //        return RedirectToAction("Index");
        //    }
        //    return View(project);
        //}

        ////
        //// GET: /Admin/Project/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Project project = ProjectDAL.FindProject(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ProjectViewModels Project = new ProjectViewModels()
        //    {
        //        ProjectId = project.ProjectId,
        //        ProjectName = project.ProjectName,
        //        Description = project.ProjectDescription
        //    };
        //    return View(Project);
        //}

        ////
        //// POST: /Admin/Project/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ProjectDAL.DeleteProject(id);
        //    return RedirectToAction("Index");
        //}
        #endregion
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