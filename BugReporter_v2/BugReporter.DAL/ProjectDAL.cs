using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BugReporter_v2.DAL
{
    public static class ProjectDAL
    {
        /// <summary>
        /// Select project from DB
        /// </summary>
        /// <param name="projectName"> project name</param>
        /// <returns> project</returns>
        public static Project FindProject(object project)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Projects.Find(project);
        }
        public static void AddProject(string projectName , string projectDescription)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            Project newProject = new Project() 
            {
                ProjectName = projectName,
                ProjectDescription = projectDescription,
            };
            db.Projects.Add(newProject);
            db.SaveChanges();
        }
        public static void EditProject(int project,string name,string description)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var findProject = db.Projects.Find(project);
            findProject.ProjectName = name;
            findProject.ProjectDescription = description;
            db.SaveChanges();
        }
        public static List<Project> AllProject()
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Projects.ToList();
        }
        public static Project FindProject(int id)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            Project project = db.Projects.Find(id);
            return project;
        }
        public static void DeleteProject(int id)
        {
            LogDAL.DeleteAllLogsForProject(id);
            BugDAL.DeleteBugsToProject(id);
            DeleteAllUsersForProject(id);
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                Project project = db.Projects.Where(x => x.ProjectId == id).Select(x => x).FirstOrDefault();
                db.Projects.Remove(project);
                db.SaveChanges();
            }
        }

        public static List<Project> GetAllProject()
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            return db.Projects.Select(x => x).Distinct().ToList();
        }
        public static bool IsProjectExist(string ProjectName, string Description)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var project = db.Projects.Where(x => x.ProjectName.Equals(ProjectName) && x.ProjectDescription
                .Equals(Description)).Select(x => x).FirstOrDefault();
            if (project == null) return false;
            else return true;
        }
        public static void DeleteAllUserProjects(int userId)
        {
            BugReporter_v2Entities db = new BugReporter_v2Entities();
            var userProjects = db.UserProfiles.Where(x => x.UserId == userId).Select(x => x).FirstOrDefault();
            var projects = userProjects.Projects.ToList();
            projects.ForEach(project => userProjects.Projects.Remove(project));
            db.SaveChanges();
        }
        public static void DeleteAllUsersForProject(int id)
        {
            using (BugReporter_v2Entities db = new BugReporter_v2Entities())
            {
                Project project = db.Projects.Where(x => x.ProjectId == id).Select(x => x).FirstOrDefault();
                var usersInProject = project.UserProfiles.ToList();
                usersInProject.ForEach(user => project.UserProfiles.Remove(user));
                db.SaveChanges();
            }
        }
    }
}
