using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class EmployeeViewModel
    {
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Cell Number")]
        public string CellNumber { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passowrd and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name ="Employee Image")]
        public string Image { get; set; }
    }
}