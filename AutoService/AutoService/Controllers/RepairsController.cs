using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoService.Models;

namespace AutoService.Controllers
{
    public class RepairsController : Controller
    {
        private AutoServiceDBEntities db = new AutoServiceDBEntities();

        // GET: Repairs
        public ActionResult Index()
        {
            var repair = db.Repair.Include(r => r.Car).Include(r => r.Employee).Include(r => r.Warranty);
            return View(repair.ToList());
        }

        // GET: Repairs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair repair = db.Repair.Find(id);
            if (repair == null)
            {
                return HttpNotFound();
            }
            return View(repair);
        }

        // GET: Repairs/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Car, "CarID", "Brand");
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FullName");
            ViewBag.WarrantyID = new SelectList(db.Warranty, "WarrantyID", "Duration");
            return View();
        }

        // POST: Repairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RepairID,CarID,WarrantyID,EmployeeID,StartDate,EndDate,Cost,Status")] Repair repair)
        {
            if (ModelState.IsValid)
            {
                db.Repair.Add(repair);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Car, "CarID", "Brand", repair.CarID);
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FullName", repair.EmployeeID);
            ViewBag.WarrantyID = new SelectList(db.Warranty, "WarrantyID", "Duration", repair.WarrantyID);
            return View(repair);
        }

        // GET: Repairs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair repair = db.Repair.Find(id);
            if (repair == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Car, "CarID", "Brand", repair.CarID);
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FullName", repair.EmployeeID);
            ViewBag.WarrantyID = new SelectList(db.Warranty, "WarrantyID", "Duration", repair.WarrantyID);
            return View(repair);
        }

        // POST: Repairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RepairID,CarID,WarrantyID,EmployeeID,StartDate,EndDate,Cost,Status")] Repair repair)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repair).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Car, "CarID", "Brand", repair.CarID);
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FullName", repair.EmployeeID);
            ViewBag.WarrantyID = new SelectList(db.Warranty, "WarrantyID", "Duration", repair.WarrantyID);
            return View(repair);
        }

        // GET: Repairs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair repair = db.Repair.Find(id);
            if (repair == null)
            {
                return HttpNotFound();
            }
            return View(repair);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repair repair = db.Repair.Find(id);
            db.Repair.Remove(repair);
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
    }
}
