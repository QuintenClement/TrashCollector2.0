using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTrash.Models;

namespace TestTrash.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Test);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ZipCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ApplicationUserId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", employee.ApplicationUserId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", employee.ApplicationUserId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ZipCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", employee.ApplicationUserId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
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
       public ActionResult FilterByZip()
        {
            string today = DateTime.Today.DayOfWeek.ToString();
            var userId = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(e => e.ApplicationUserId == userId).Single();
            var customers = db.Customers.Where(c => c.ZipCode == employee.ZipCode && c.DayAvailable.Equals(today)).ToList();
            

            ViewBag.Days = new List<SelectListItem>()
            {
                new SelectListItem {Text="Select",Value=null,Selected=true },
                new SelectListItem {Text="Monday",Value="Monday" },
                new SelectListItem {Text="Tuesday",Value="Tuesday" },
                new SelectListItem {Text="Wednesday",Value="Wednesday" },
                new SelectListItem {Text="Thursday",Value="Thursday" },
                new SelectListItem {Text="Friday",Value="Friday" },
                new SelectListItem {Text="Saturday",Value="Saturday" },
                new SelectListItem {Text="Sunday",Value="Sunday" },
            };


            ViewModel vm = new ViewModel();
            vm.customers = customers;

            return View(vm);
        }

        [HttpPost]
        public ActionResult FilterByZip(ViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(e => e.ApplicationUserId == userId).Single();
            var customers = db.Customers.Where(c => c.ZipCode == employee.ZipCode && c.DayAvailable == viewModel.DaySelection).ToList();


            ViewBag.Days = new List<SelectListItem>()
            {
                new SelectListItem {Text="Select",Value=null,Selected=true },
                new SelectListItem {Text="Monday",Value="Monday" },
                new SelectListItem {Text="Tuesday",Value="Tuesday" },
                new SelectListItem {Text="Wednesday",Value="Wednesday" },
                new SelectListItem {Text="Thursday",Value="Thursday" },
                new SelectListItem {Text="Friday",Value="Friday" },
                new SelectListItem {Text="Saturday",Value="Saturday" },
                new SelectListItem {Text="Sunday",Value="Sunday" },
            };
            viewModel.customers = customers;
            return View(viewModel);
        }

        public ActionResult ConfirmPickup(int? id)
        {
            
            Customer customer = db.Customers.Find(id);
            customer.Status = "Picked up";
            ChargeCustomer(id);
            db.SaveChanges();
            return RedirectToAction("FilterByZip");
        }
        public void ChargeCustomer(int? id)
        {
            Customer customer = db.Customers.Find(id);
            customer.AmountOwed += 2.50;
        }
       
       
    }
}
