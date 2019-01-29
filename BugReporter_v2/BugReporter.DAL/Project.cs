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
    
    public partial class Project
    {
        public Project()
        {
            this.Bugs = new HashSet<Bug>();
            this.UserProfiles = new HashSet<UserProfile>();
        }
    
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
    
        public virtual ICollection<Bug> Bugs { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
