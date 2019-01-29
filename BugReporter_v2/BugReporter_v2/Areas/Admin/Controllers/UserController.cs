using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BugReporter_v2.Filters;
using BugReporter_v2.Models;
using BugReporter_v2.DAL;
using BugReporter_v2.Areas.Admin.Models;
using System.IO.Compression;
using System.Web.Routing;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;



namespace BugReporter_v2.Areas.Admin.Controllers
{
    [Authorize(Roles="Administrator")]
    public class UserController : Controller
    {

        //
        // GET: /Admin/UserManagement/

        public ActionResult Index()
        {

            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
                List<BugReporter_v2.DAL.UserProfile> getAllUsers = UserDAL.GetAllUsers();
                List<RegisterModel> allUsers = new List<RegisterModel>();
                foreach (var item in getAllUsers)
                {
                    if (item.IsActive)
                    {
                        RegisterModel user = new RegisterModel()
                        {
                            UserId = item.UserId,
                            UserName = item.UserName,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            Telephone = item.Telephone,
                            Password = String.Empty,
                            ConfirmPassword = String.Empty
                        };
                        allUsers.Add(user);
                    }
                }
                return View(allUsers);
        }


        public ActionResult UsersActivities()
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            List<BugReporter_v2.DAL.UserProfile> getAllUsers = UserDAL.GetAllUsers();
            List<AllUsersActivityDetails> allUsers = new List<AllUsersActivityDetails>();
            foreach (var item in getAllUsers)
            {

                string lastActivity = item.Logs.Count==0 ? "No activity done yet" :
                    item.Logs.Last().Activity + "  #" + item.Logs.Last().BugId + " Bug";
                int numberOfProjects = item.Projects.Count == 0 ? 0 : item.Projects.Count;
                int numberOfBugs = item.Bugs.Count == 0 ? 0 : item.Bugs.Count;
                AllUsersActivityDetails user = new AllUsersActivityDetails()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    LastActivityDone = lastActivity, 
                    LastActivityTime = item.LastActivityTime,
                    NumberOfProjects = numberOfProjects,
                    NumberOfBugs = numberOfBugs
                };
                allUsers.Add(user);
            }
            return View(allUsers);
        }
        
        // POST: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        [Compress]
        [AcceptVerbs(HttpVerbs.Post)]
        [InitializeSimpleMembership]
        public ActionResult Register(RegisterModel model)
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                    {
                        Email = model.Email,
                        Telephone = model.Telephone,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        IsActive = true,
                    });
                    Roles.AddUserToRole(model.UserName,"User");
                    RouteValueDictionary routeValues = this.GridRouteValues();
                    return RedirectToAction("Index", routeValues);
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult Edit(string UserName,int UserId)
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            var user = BugReporter_v2.DAL.UserDAL.GetUserById(UserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            EditUserModel userInfo = new EditUserModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = UserId

            };
            ViewBag.UserId = UserId;
            return View(userInfo);
        }

        [HttpPost]
        [InitializeSimpleMembership]
        public ActionResult Edit(EditUserModel model)
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "../Home");
            }
            if (ModelState.IsValid)
            {
                var user = BugReporter_v2.DAL.UserDAL.GetUserById(model.UserId);
                if (user == null)
                {
                    return HttpNotFound();
                }


                BugReporter_v2.DAL.UserDAL.EditUserInfo(model.UserName,
                    model.FirstName, model.LastName, model.Email, model.Telephone, model.UserId);
                BugReporter_v2.DAL.UserDAL.EditUserInfo(model.UserName, model.FirstName,
                    model.LastName, model.Email, model.Telephone, model.UserId);

                if (!String.IsNullOrWhiteSpace(model.Password))
                {
                    WebSecurity.ChangePassword(model.UserName, model.OldPassword, model.Password);
                }


                List<BugReporter_v2.DAL.UserProfile> getAllUsers = UserDAL.GetAllUsers();
                List<RegisterModel> allUsers = new List<RegisterModel>();
                foreach (var item in getAllUsers)
                {
                    RegisterModel userAdd = new RegisterModel()
                    {
                        UserId = item.UserId,
                        UserName = item.UserName,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Telephone = item.Telephone,
                        Password = String.Empty,
                        ConfirmPassword = String.Empty
                    };
                    allUsers.Add(userAdd);
                }
                return View("Index", allUsers);


            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //[Compress]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!model.Password.Equals(String.Empty))
        //        {
        //            WebSecurity.ChangePassword(model.UserName, model.ConfirmPassword, model.Password);
        //        }
        //        BugReporter_v2.DAL.UserDAL.EditUserInfo(model.UserName, model.FirstName, model.LastName, model.Email, model.Telephone,model.UserId);
        //        RouteValueDictionary routeValues = this.GridRouteValues();
        //        return RedirectToAction("Index", routeValues);
        //    }
        //    else
        //    {
        //        TempData["message"] = string.Format("Server error. Please try again!");
        //        List<BugReporter_v2.DAL.UserProfile> getAllUsers = UserDAL.GetAllUsers();
        //        List<RegisterModel> allUsers = new List<RegisterModel>();
        //        foreach (var item in getAllUsers)
        //        {
        //            RegisterModel user = new RegisterModel()
        //            {
        //                UserId = item.UserId,
        //                UserName = item.UserName,
        //                FirstName = item.FirstName,
        //                LastName = item.LastName,
        //                Email = item.Email,
        //                Telephone = item.Telephone,
        //                Password = String.Empty,
        //                ConfirmPassword = String.Empty
        //            };
        //            allUsers.Add(user);
        //        }
        //        return View("Index",allUsers);
        //    }
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        [InitializeSimpleMembership]
        public ActionResult Delete(int UserId = 0)
        {
 
            var user = BugReporter_v2.DAL.UserDAL.FindUser(UserId);
            RouteValueDictionary routeValues;
            bool isAdmin = Roles.IsUserInRole(user.UserName, "Administrator");
            if (user == null || isAdmin)
            {
                routeValues = this.GridRouteValues();

                return RedirectToAction("Index", routeValues);
            }
            var deletedUser = UserDAL.GetUserById(UserId);
            string name = deletedUser.UserName;
            BugDAL.DeleteBugsToUser(UserId);
            LogDAL.DeleteLogsForUser(UserId);
            Roles.RemoveUserFromRole(name, "User");
            ProjectDAL.DeleteAllUserProjects(UserId);
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(name);
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(name, true);
            routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
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

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}



#region
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Transactions;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;
//using BugReporter_v2.Filters;
//using BugReporter_v2.Models;
//using BugReporter_v2.DAL;
//using BugReporter_v2.Areas.Admin.Models;
//using System.IO.Compression;

//namespace BugReporter_v2.Areas.Admin.Controllers
//{
//    public class UserController : Controller
//    {

//        //
//        // GET: /Admin/UserManagement/

//        public ActionResult Index()
//        {
//            List<BugReporter_v2.DAL.UserProfile> getAllUsers = UserDAL.GetAllUsers();
//            List<UserViewModel> allUsers = new List<UserViewModel>();
//            foreach (var item in getAllUsers)
//            {
//                UserViewModel user = new UserViewModel()
//                {
//                    UserId = item.UserId,
//                    UserName = item.UserName,
//                    FirstName = item.FirstName,
//                    LastName = item.LastName,
//                    Email = item.Email,
//                    Telephone = item.Telephone
//                };
//                allUsers.Add(user);
//            }
//            return View(allUsers);
//        }

//        public ActionResult Details(int id)
//        {
//            var user = BugReporter_v2.DAL.UserDAL.GetUserById(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            UserViewModel userInfo = new UserViewModel()
//            {
//                UserName = user.UserName,
//                Email = user.Email,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Telephone = user.Telephone,
//                UserId = id

//            };
//            return View(userInfo);

//        }
//        //
//        // GET: Admin/UserManagement/Register

//        public ActionResult Register()
//        {
//            return View();
//        }




//        //
//        // POST: /Account/Register

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [InitializeSimpleMembership]
//        public ActionResult Register(RegisterModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Attempt to register the user
//                try
//                {
//                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
//                    {
//                        Email = model.Email,
//                        Telephone = model.Telephone,
//                        FirstName = model.FirstName,
//                        LastName = model.LastName
//                    });
//                    return RedirectToAction("Index", "User");
//                }
//                catch (MembershipCreateUserException e)
//                {
//                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }


//        public ActionResult Edit(int id)
//        {
//            var user = BugReporter_v2.DAL.UserDAL.GetUserById(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            UserViewModel userInfo = new UserViewModel()
//            {
//                UserName = user.UserName,
//                Email = user.Email,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Telephone = user.Telephone,
//                UserId = id

//            };
//            ViewBag.UserId = id;
//            return View(userInfo);
//        }

//        [HttpPost]
//        public ActionResult Edit(UserViewModel model, int id)
//        {
//            if (ModelState.IsValid)
//            {
//                BugReporter_v2.DAL.UserDAL.EditUserInfo(model.UserName, model.FirstName, model.LastName, model.Email, model.Telephone, id);
//                return RedirectToAction("Index", "User");
//            }
//            else
//            {
//                TempData["message"] = string.Format("Server error. Please try again!");
//                var user = BugReporter_v2.DAL.UserDAL.GetUserById(id);
//                UserViewModel userInfo = new UserViewModel()
//                {
//                    UserName = user.UserName,
//                    Email = user.Email,
//                    FirstName = user.FirstName,
//                    LastName = user.LastName,
//                    Telephone = user.Telephone,
//                    UserId = id

//                };
//                ViewBag.UserId = id;
//                return View(userInfo);
//            }
//        }

//        public ActionResult Delete(int id = 0)
//        {
//            var user = BugReporter_v2.DAL.UserDAL.FindUser(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            UserViewModel userInfo = new UserViewModel()
//            {
//                UserName = user.UserName,
//                Email = user.Email,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Telephone = user.Telephone,
//                UserId = id

//            };
//            ViewBag.id = id;
//            return View(userInfo);
//        }

//        [HttpPost, ActionName("Delete")]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            UserDAL.DeleteUser(id);
//            return RedirectToAction("Index");
//        }

//        //
//        // GET: /Account/Manage

//        public ActionResult Manage(ManageMessageId? message)
//        {
//            ViewBag.StatusMessage =
//                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
//                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
//                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
//                : "";
//            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
//            ViewBag.ReturnUrl = Url.Action("Manage");
//            return View();
//        }

//        //
//        // POST: /Account/Manage

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Manage(LocalPasswordModel model)
//        {
//            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
//            ViewBag.HasLocalPassword = hasLocalAccount;
//            ViewBag.ReturnUrl = Url.Action("Manage");
//            if (hasLocalAccount)
//            {
//                if (ModelState.IsValid)
//                {
//                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
//                    bool changePasswordSucceeded;
//                    try
//                    {
//                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
//                    }
//                    catch (Exception)
//                    {
//                        changePasswordSucceeded = false;
//                    }

//                    if (changePasswordSucceeded)
//                    {
//                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
//                    }
//                    else
//                    {
//                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
//                    }
//                }
//            }
//            else
//            {
//                // User does not have a local password so remove any validation errors caused by a missing
//                // OldPassword field
//                ModelState state = ModelState["OldPassword"];
//                if (state != null)
//                {
//                    state.Errors.Clear();
//                }

//                if (ModelState.IsValid)
//                {
//                    try
//                    {
//                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
//                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
//                    }
//                    catch (Exception e)
//                    {
//                        ModelState.AddModelError("", e);
//                    }
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        public class CompressAttribute : ActionFilterAttribute
//        {
//            public override void OnActionExecuting(ActionExecutingContext filterContext)
//            {

//                var encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
//                if (string.IsNullOrEmpty(encodingsAccepted)) return;

//                encodingsAccepted = encodingsAccepted.ToLowerInvariant();
//                var response = filterContext.HttpContext.Response;

//                if (encodingsAccepted.Contains("deflate"))
//                {
//                    response.AppendHeader("Content-encoding", "deflate");
//                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
//                }
//                else if (encodingsAccepted.Contains("gzip"))
//                {
//                    response.AppendHeader("Content-encoding", "gzip");
//                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
//                }
//            }
//        }

//        #region Helpers
//        private ActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }
//        }

//        public enum ManageMessageId
//        {
//            ChangePasswordSuccess,
//            SetPasswordSuccess,
//            RemoveLoginSuccess,
//        }

//        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
//        {
//            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
//            // a full list of status codes.
//            switch (createStatus)
//            {
//                case MembershipCreateStatus.DuplicateUserName:
//                    return "User name already exists. Please enter a different user name.";

//                case MembershipCreateStatus.DuplicateEmail:
//                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

//                case MembershipCreateStatus.InvalidPassword:
//                    return "The password provided is invalid. Please enter a valid password value.";

//                case MembershipCreateStatus.InvalidEmail:
//                    return "The e-mail address provided is invalid. Please check the value and try again.";

//                case MembershipCreateStatus.InvalidAnswer:
//                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

//                case MembershipCreateStatus.InvalidQuestion:
//                    return "The password retrieval question provided is invalid. Please check the value and try again.";

//                case MembershipCreateStatus.InvalidUserName:
//                    return "The user name provided is invalid. Please check the value and try again.";

//                case MembershipCreateStatus.ProviderError:
//                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

//                case MembershipCreateStatus.UserRejected:
//                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

//                default:
//                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
//            }
//        }
//        #endregion
//    }
//}

#endregion