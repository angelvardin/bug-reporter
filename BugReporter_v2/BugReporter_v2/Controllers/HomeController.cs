using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BugReporter_v2.DAL;
using WebMatrix.WebData;




namespace BugReporter_v2.Controllers
{
    public class HomeController : Controller
    {
        [Compress]
        [Authorize]
        public ActionResult Index()
        {
            var _Roles = (SimpleRoleProvider)Roles.Provider;
            var roles = _Roles.IsUserInRole(User.Identity.Name, "Administrator"); 
  
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            if (roles || _Roles.UserIdColumn==null)
            {
                return RedirectToAction("Index", "Admin/User/");
                //return Redirect("Admin/User/" ,"Index");
                
            }
            ViewBag.bjhb = User.Identity.AuthenticationType;
            var userLogs = LogDAL.GetAllLogsForUser(User.Identity.Name);
            List<BugReporter_v2.Areas.Admin.Models.UsersActivityModel> activities =
                new List<BugReporter_v2.Areas.Admin.Models.UsersActivityModel>();
            if (userLogs.Count == 0)
            {
                BugReporter_v2.Areas.Admin.Models.UsersActivityModel activity =
                new BugReporter_v2.Areas.Admin.Models.UsersActivityModel()
                {
                    Id = 1,
                    LastActivityDone = "No activity done yet!",
                    Project = "None",
                };
                activities.Add(activity);
            }
            else
            {
                int i = 1;
                foreach (var item in userLogs)
                {
                    BugReporter_v2.Areas.Admin.Models.UsersActivityModel activity =
                    new BugReporter_v2.Areas.Admin.Models.UsersActivityModel()
                    {
                        Id = i,
                        LastActivityDone = item.Activity + " #" + item.BugId + " Bug ",
                        Project = item.Bug.Project.ProjectName,
                    };
                    i++;
                    activities.Add(activity);
                }
            }
            ViewBag.TotalBugs = userLogs.Where(x => x.Activity.Equals("Add")).Count();
            ViewBag.Projects = UserDAL.GetCountOfProjects(User.Identity.Name);
            ViewBag.LastActivity = userLogs.Select(x => x.UserProfile.LastActivityTime).LastOrDefault();
            UserDAL.AddActivity(User.Identity.Name);
            return View(activities);
                
               
        }
        [Compress]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            if (User.Identity.IsAuthenticated)
            {
                UserDAL.AddActivity(User.Identity.Name);
            }

            return View();
        }
        [Compress]
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (User.Identity.IsAuthenticated)
            {
                UserDAL.AddActivity(User.Identity.Name);
            }

            return View();
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
    }
}
