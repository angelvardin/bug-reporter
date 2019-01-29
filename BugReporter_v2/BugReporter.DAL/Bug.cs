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
    
    public partial class Bug
    {
        public Bug()
        {
            this.Logs = new HashSet<Log>();
        }
    
        public int BugId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Status { get; set; }
        public string BugDescription { get; set; }
        public string Priority { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public System.DateTime DateOfFirstSubmit { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
