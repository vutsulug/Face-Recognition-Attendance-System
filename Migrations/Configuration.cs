namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.AttendanceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.Models.AttendanceDbContext context)
        {
            context.Attendance.AddOrUpdate(x => x.AttendanceID,
            new Attendance() { AttendanceID = 1, CheckInDateTime = DateTime.UtcNow, DisplayMessage = "Right on time", EmployeeID = 5,CheckOutDateTime = DateTime.UtcNow, IsDeleted = false },
            new Attendance() { AttendanceID = 2, CheckInDateTime = DateTime.UtcNow, DisplayMessage = "Right on time", EmployeeID = 2, CheckOutDateTime = DateTime.UtcNow, IsDeleted = false },
            new Attendance() { AttendanceID = 3, CheckInDateTime = DateTime.UtcNow, DisplayMessage = "Right on time", EmployeeID = 1, CheckOutDateTime = DateTime.UtcNow, IsDeleted = false },
            new Attendance() { AttendanceID = 4, CheckInDateTime = DateTime.UtcNow, DisplayMessage = "Right on time", EmployeeID = 3, CheckOutDateTime = DateTime.UtcNow, IsDeleted = false },
            new Attendance() { AttendanceID = 5, CheckInDateTime = DateTime.UtcNow, DisplayMessage = "Right on time", EmployeeID = 4, CheckOutDateTime = DateTime.UtcNow,IsDeleted=false });


            context.Employees.AddOrUpdate(x => x.EmployeeID,
            new Employee { FirstName = "Carson", Surname = "Alexander", CellNumber = "0756368545", EmailAddress = "Carson.Alexander@email.com", IsDeleted = false, EmployeeID = 1,DisplayName= "Carson Alexander",EmployeeCode = "G63787" },
            new Employee { FirstName = "Mike", Surname = "Blank", CellNumber = "0756368545", EmailAddress = "Mike.Blank@email.com", IsDeleted = false, EmployeeID = 2,DisplayName="Mike Blank",EmployeeCode="C63987" },
            new Employee { FirstName = "Jane", Surname = "Sanders", CellNumber = "0756368545", EmailAddress = "Jane.Sanders@email.com", IsDeleted = false, EmployeeID = 3,DisplayName="Jane Sanders",EmployeeCode="M34125" },
            new Employee { FirstName = "Simon", Surname = "Jimp", CellNumber = "0756368545", EmailAddress = "Simon.Jimp@email.com", IsDeleted = false, EmployeeID = 4,DisplayName="Simon Jimp",EmployeeCode="A20045" },
            new Employee { FirstName = "Linda", Surname = "McGru", CellNumber = "0756368545", EmailAddress = "Linda.Mcgru@email.com", IsDeleted = false, EmployeeID = 5,DisplayName="Linda McGru",EmployeeCode="Y96532" });
        
    }
    }
}
