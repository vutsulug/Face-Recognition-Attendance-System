using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public string DisplayMessage { get; set; }
        public bool IsDeleted { get; set; }


        [ForeignKey(nameof(EmployeeID))]
        public Employee Employee { get; set; }
    }
}