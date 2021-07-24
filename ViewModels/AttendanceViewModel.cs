using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class AttendanceViewModel
    {
        public int AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public string DisplayMessage { get; set; }
        public bool IsDeleted { get; set; }

        public List<Employee> Employee { get; set; }
        public string ReturnUrl { get; set; }

    }
}