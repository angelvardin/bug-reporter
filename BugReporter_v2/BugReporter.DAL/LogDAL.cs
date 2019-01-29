using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BugReporter_v2.DAL
{
    public static class LogDAL
    {
        
        public static void AddLogForBug(string user, int bugId , string actvity)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var User = db.UserProfiles.Where(x => x.UserName.Equals(user)).FirstOrDefault();
            Log log = new Log()
            {
                BugId = bugId,
                UserId = User.UserId,
                Time = DateTime.Now,
                Activity = actvity,
            };
            db.Logs.Add(log);
            db.SaveChanges();
        }

        public static List<Log> GetAllLogsForUser(string username)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Logs.Where(x => x.UserProfile.UserName.Equals(username)).Select(x => x).ToList();
        }
        public static void DeleteLogsForUser(int userId)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var logs= db.Logs.Where(x => x.UserId==userId).Select(x => x);
            foreach (var item in logs)
            {
                db.Logs.Remove(item);
            }
            db.SaveChanges();
        }
        public static void DeleteLogsForBug(int bugId)
        {
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                var log = db.Logs.Where(x => x.BugId == bugId).Select(x => x).ToList();
                foreach (var item in log)
                {
                    db.Logs.Remove(item);
                }
                db.SaveChanges();
            }
        }

        //not working
        public static void DeleteAllLogsForProject(int id)
        {
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                var selectAllBugsToProject = db.Bugs.Include("Logs").Where(x => x.ProjectId == id).Select(x => x);
                foreach (var item in selectAllBugsToProject)
                {
                    DeleteLogsForBug(item.BugId);
                    //var logs = item.Logs.ToList();
                    //logs.ForEach(log => item.Logs.Remove(log));
                    //db.Bugs.Remove(item);
                }
                db.SaveChanges();
            }
        }
    }
}
