using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TestTrash.Models;


namespace TestTrash.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers;
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DayAvailable,Address,ZipCode")] Customer customers)
        {
            if (ModelState.IsValid)
            {
                customers.ApplicationUserId = User.Identity.GetUserId();
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", customers.ApplicationUserId);
            return View(customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", customers.ApplicationUserId);
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,DayAvailable,ZipCode,ExtraPickup,StartSuspend,EndSuspend")] Customer customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", customers.ApplicationUserId);
            return View(customers);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
      
        public ActionResult GetLatLngOfCustomer()
        {
            foreach (var customers in db.Customers)
            {
                string address = customers.Address;
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}+&key=AIzaSyBpYq3xWWk8w9uCxu3-NG_PjUArik5Z8uM", Uri.EscapeDataString(address));
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());
                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                XElement locationElement = result.Element("geometry").Element("location");
                customers.Latitude = double.Parse(locationElement.Element("lat").ToString());
                customers.Longitude = double.Parse(locationElement.Element("lng").ToString());
                db.SaveChanges();
                return RedirectToAction("FilteryByZip", "Employees");

            }
            return RedirectToAction("FilteryByZip", "Employees");

        }
    }
}