using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BugReporter_v2.DAL
{
    public static class UserDAL
    {
        
        public static UserProfile GetUserById(int userId)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.UserProfiles.Where(x => x.UserId == userId).Select(x => x).FirstOrDefault();

        }
        public static UserProfile FindUser(int id)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.UserProfiles.Find(id);
        }

        public static UserProfile FindUserByName(string name)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.UserProfiles.Find(name);
        }

        public static void EditUserInfo(string username, string firstName, string lastName, string email, string telephone, int id)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var user = db.UserProfiles.Where(x => x.UserId == id).Select(x => x).FirstOrDefault();
            user.UserName = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Telephone = telephone;
            db.SaveChanges();
        }
        public static void DeleteUser(int id)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            UserProfile User = db.UserProfiles.Where(x => x.UserId == id).Select(x => x).FirstOrDefault();
            User.IsActive = false;
            db.SaveChanges();
        }

        public static List<UserProfile> GetAllUsers()
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.UserProfiles.Select(x => x).ToList();
        }
        public static UserProfile GetUserByName(string userName)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.UserProfiles.Where(x =>x.UserName.Equals(userName)).Select(x => x).FirstOrDefault();
        }
        public static void AddActivity(string user)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var User = db.UserProfiles.Where(x => x.UserName.Equals(user)).Select(x => x).FirstOrDefault();
            User.LastActivityTime = DateTime.Now;
            db.SaveChanges();
        }
        public static int GetCountOfProjects(string username)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.UserProfiles.Where(x =>x.UserName.Equals(username)).Select(x => x.Projects).Count();
        }
  
    }
}
