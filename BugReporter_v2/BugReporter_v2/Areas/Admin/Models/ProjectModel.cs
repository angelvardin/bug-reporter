using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace BugReporter_v2.Areas.Admin.Models
{
    public class ProjectViewModels
    {
        [ScaffoldColumn(false)]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "An Project name is required")]
        [DataType(DataType.Text)]
        [DisplayName("Project name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "An Description is required")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Description { get; set; }
    }
}