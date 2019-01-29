using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugReporter_v2.DAL
{
    public static class BugDAL
    {

        /// <summary>
        /// Submitting a bug to project
        /// </summary>
        /// <param name="project">project name </param>
        /// <param name="user">user name</param>
        ///
        public static void SubmitBugToProject(int projectId, string user, string status, string description, string priority)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var User = db.UserProfiles.Include("Projects").Where(x => x.UserName.Equals(user)).Select(x => x).FirstOrDefault();
            var project = db.Projects.Where(x => x.ProjectId == projectId).Select(x => x).FirstOrDefault();
            var userID = User.UserId;
            Bug newBug = new Bug()
            {
                UserId = userID,
                Status = status,
                BugDescription = description,
                Priority = priority,
                ProjectId = projectId,
                DateOfFirstSubmit = DateTime.Now
            };
            db.Bugs.Add(newBug);
            User.Projects.Add(project);
            db.SaveChanges();
        }

        

        public static void EditBug(int projectID, int userID, string status, string description, string priority, int bugId)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var selectBug = db.Bugs.Where(x => bugId == x.BugId).FirstOrDefault();
            selectBug.ProjectId = projectID;
            selectBug.UserId = userID;
            selectBug.Status = status;
            selectBug.BugDescription = description;
            selectBug.Priority = priority;
            db.SaveChanges();

        }
        public static List<Bug> GetLastAddedBug()
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Bugs.OrderByDescending(x => x.BugId).Select(x => x).Take(1).ToList();

        }

        public static Bug GetBugByID(int id)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Bugs.Where(x => x.BugId == id).Select(x => x).FirstOrDefault();

        }
        public static List<Bug> GetAllBugs()
        {
            List<Bug> allBug = null;
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                 allBug = db.Bugs.Include("Project")
                    .Include("UserProfile").Where(x => !x.Status.Equals("Closed") && !x.Status.Equals("Deleted")).ToList();
            }
            return allBug;
        }
        public static void DeleteBug(int BugId)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            Bug bug = db.Bugs.Where(x => x.BugId == BugId).Select(x => x).FirstOrDefault();
            bug.Status = "Deleted";
            db.SaveChanges();

        }
        public static void DeleteBugFromDB(int BugId)
        {
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                Bug bug = db.Bugs.Where(x => x.BugId == BugId).Select(x => x).FirstOrDefault();
                db.Bugs.Remove(bug);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// Select all bugs from given project
        /// </summary>
        /// <param name="project">project name</param>
        /// <returns>list</returns>
        public static List<Bug> SelectAllBugsFromProject(int projectId)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Bugs.Where(x => x.ProjectId == projectId &&!x.Status.Equals("Deleted")
                &&!x.Status.Equals("Closed")).Select(x => x).ToList();
        }
        public static void DeleteBugsToUser(int id)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var userBugs = db.Bugs.Where(x => x.UserId == id).Select(x =>x);
            foreach (var item in userBugs)
            {
                item.UserId = null;
            }
            db.SaveChanges();
        }
        public static void DeleteBugsToProject(int id)
        {
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                var projectBugs = db.Bugs.Where(x => x.ProjectId == id).Select(x => x).ToList();
                foreach (var item in projectBugs)
                {
                    DeleteBugFromDB(item.BugId);
                }

                db.SaveChanges();
            }
        }
    }
}
