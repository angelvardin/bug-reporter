using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugReporter_v2.DAL;

namespace BugReporter_v2.Models
{


     public class CreateBugModel
    {

        [Required(ErrorMessage = "An Proirity is required")]
        [DisplayName("Proirity")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "An Description is required")]
        [DisplayName("Description")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Desctiption { get; set; }

        [Required(ErrorMessage = "An Project Name is required")]
        [DisplayName("Project")]
        public int ProjectID { get; set; }

    }
    public class EditBugModel
    {
        
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("User")]
        public int Owner { get; set; }

        [Required(ErrorMessage = "An Proirity is required")]
        [DisplayName("Proirity")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "An Description is required")]
        [DisplayName("Description")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Desctiption { get; set; }

        [Required(ErrorMessage = "An Status is required")]
        [DisplayName("Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "An Project Name is required")]
        [DisplayName("Project")]
        public int ProjectID { get; set; }


    }

    public class BugViewModel
    {

        [DisplayName("ID")]
        public int BugId { get; set; }

        [ScaffoldColumn(false)]
        public int ProjectId { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("User")]
        public string Owner { get; set; }

   
        [DisplayName("Proirity")]
        public string Priority { get; set; }

        [DisplayName("Description")]
        [StringLength(50)]
        public string Desctiption { get; set; }

        
        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Date")]
        public DateTime DateOfFirstSubmit { get; set; }

  
    }

    public class BugAndUsersViewModel
    {

        [DisplayName("ID")]
        public int BugId { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("User")]
        public string Owner { get; set; }


        [DisplayName("Proirity")]
        public string Priority { get; set; }

        [DisplayName("Description")]
        [StringLength(50)]
        public string Desctiption { get; set; }


        [DisplayName("Status")]
        public string Status { get; set; }

    }

}
