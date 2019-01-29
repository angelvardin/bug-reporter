//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BugReporter_v2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Bugs = new HashSet<Bug>();
            this.Logs = new HashSet<Log>();
            this.Projects = new HashSet<Project>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> LastActivityTime { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<Bug> Bugs { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
