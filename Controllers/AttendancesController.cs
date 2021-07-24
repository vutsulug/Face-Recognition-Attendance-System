using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AttendancesController : Controller
    {
        private AttendanceDbContext db = new AttendanceDbContext();

        // GET: Attendances
        public async Task<ActionResult> Index()
        {
            var attendance = db.Attendance.Include(a => a.Employee);
            return View(await attendance.Where(x => x.Employee.IsDeleted != true && x.IsDeleted != true).ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = await db.Attendance.Include(c => c.Employee).Where(x => x.AttendanceID == id).SingleOrDefaultAsync();
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create(string returnUrl = "")
        {
            var model = new AttendanceViewModel { ReturnUrl = returnUrl };
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeCode");
            return View(model);
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AttendanceViewModel attendance)
        {
            if (ModelState.IsValid)
            {
                var item = new Attendance();
                item.CheckInDateTime = attendance.CheckInDateTime;
                item.CheckOutDateTime = attendance.CheckOutDateTime;
                item.DisplayMessage = attendance.DisplayMessage;
                item.EmployeeID = attendance.EmployeeID;
                
                db.Attendance.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeCode", attendance.EmployeeID);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = await db.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeCode", attendance.EmployeeID);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AttendanceID,EmployeeID,CheckInDateTime,CheckOutDateTime,DisplayMessage")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeCode", attendance.EmployeeID);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = await db.Attendance.Include(x => x.Employee).Where(c => c.AttendanceID == id).SingleOrDefaultAsync();
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, [Bind(Include = "AttendanceID,EmployeeID,CheckInDateTime,CheckOutDateTime,DisplayMessage,Employee")] Attendance attendance)
        {
            attendance = await db.Attendance.Include(x => x.Employee).Where(c => c.AttendanceID == id).SingleOrDefaultAsync();
            attendance.IsDeleted = true;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
