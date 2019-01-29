using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace BugReporter_v2.Areas.Admin.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Еmail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
            ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Display(Name = "Tel")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
    }

    public class EditUserModel
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

     
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

  
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Еmail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
            ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Display(Name = "Tel")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}",
        //    ErrorMessage = "Invalid phone number.")]
        public string Telephone { get; set; }
    }

    public class AllUsersActivityDetails
    {
        public int UserId { get; set; }

        
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name="Number Projects")]
        public int? NumberOfProjects { get; set; }

        [Display(Name = "Number Bugs")]
        public int? NumberOfBugs { get; set; }

        [Display(Name="Last Actitity Time")]
        public DateTime? LastActivityTime { get; set; }

        [Display(Name="Last Activity")]
        public string LastActivityDone { get; set; }
    }

    public class UsersActivityModel
    {
        [Display(Name = "")]
        public int Id { get; set; }

        [Display(Name = "Last Activity")]
        public string LastActivityDone { get; set; }

        [Display(Name = "Project")]
        public string Project { get; set; }
    }

    /* public class UsersContext : DbContext
     {
         public UsersContext()
             : base("DefaultConnection")
         {
         }

         public DbSet<UserProfile> UserProfiles { get; set; }
     }

     public class RegisterModel
     {
         [Required]
         [Display(Name = "User name")]
         public string UserName { get; set; }

         [Required]
         [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
         [DataType(DataType.Password)]
         [Display(Name = "Password")]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Confirm password")]
         [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
         public string ConfirmPassword { get; set; }

         [Required]
         [DataType(DataType.EmailAddress)]
         [Display(Name = "First name")]
         public string FirstName { get; set; }

         [Required]
         [Display(Name = "Last name")]
         public string LastName { get; set; }

         [Display(Name = "Еmail")]
         [DataType(DataType.EmailAddress)]
         [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
             ErrorMessage = "Email is not valid")]
         public string Email { get; set; }

         [Display(Name = "Tel")]
         [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}",
             ErrorMessage = "Invalid phone number.")]
         public string Telephone { get; set; }
     }

     public class LocalPasswordModel
     {
         [Required]
         [DataType(DataType.Password)]
         [Display(Name = "Current password")]
         public string OldPassword { get; set; }

         [Required]
         [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
         [DataType(DataType.Password)]
         [Display(Name = "New password")]
         public string NewPassword { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Confirm new password")]
         [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
         public string ConfirmPassword { get; set; }
     }

     [Table("UserProfile")]
     public class UserProfile
     {
         [Key]
         [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
         public int UserId { get; set; }
         public string UserName { get; set; }
     }*/


}