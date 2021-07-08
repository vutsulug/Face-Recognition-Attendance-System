using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee : Admin
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string CellNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string DisplayName { get; set; }
        public string EmployeePhoto { get; set; }

        public string EmployeeCode { get; set; }
        //public LoginType LoginType { get; set; }

    }
    public class LoginType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LoginTypeID { get; set; }
        public LoginLevel Description { get; set; }
    }

    public enum LoginLevel
    {
        Administator,
        GeneralUser
    }
}
