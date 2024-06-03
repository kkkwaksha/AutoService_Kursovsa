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
    public class CarOwnersController : Controller
    {
        private AutoServiceDBEntities db = new AutoServiceDBEntities();

        // GET: CarOwners
        public ActionResult Index()
        {
            return View(db.CarOwner.ToList());
        }

        // GET: CarOwners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarOwner carOwner = db.CarOwner.Find(id);
            if (carOwner == null)
            {
                return HttpNotFound();
            }
            return View(carOwner);
        }

        // GET: CarOwners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerID,FullName,Contacts")] CarOwner carOwner)
        {
            if (ModelState.IsValid)
            {
                db.CarOwner.Add(carOwner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carOwner);
        }

        // GET: CarOwners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarOwner carOwner = db.CarOwner.Find(id);
            if (carOwner == null)
            {
                return HttpNotFound();
            }
            return View(carOwner);
        }

        // POST: CarOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerID,FullName,Contacts")] CarOwner carOwner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carOwner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carOwner);
        }

        // GET: CarOwners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarOwner carOwner = db.CarOwner.Find(id);
            if (carOwner == null)
            {
                return HttpNotFound();
            }
            return View(carOwner);
        }

        // POST: CarOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarOwner carOwner = db.CarOwner.Find(id);
            db.CarOwner.Remove(carOwner);
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
