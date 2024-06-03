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
    public class WarrantiesController : Controller
    {
        private AutoServiceDBEntities db = new AutoServiceDBEntities();

        // GET: Warranties
        public ActionResult Index()
        {
            return View(db.Warranty.ToList());
        }

        // GET: Warranties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warranty warranty = db.Warranty.Find(id);
            if (warranty == null)
            {
                return HttpNotFound();
            }
            return View(warranty);
        }

        // GET: Warranties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warranties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WarrantyID,Duration,Terms,Status")] Warranty warranty)
        {
            if (ModelState.IsValid)
            {
                db.Warranty.Add(warranty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(warranty);
        }

        // GET: Warranties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warranty warranty = db.Warranty.Find(id);
            if (warranty == null)
            {
                return HttpNotFound();
            }
            return View(warranty);
        }

        // POST: Warranties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WarrantyID,Duration,Terms,Status")] Warranty warranty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warranty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(warranty);
        }

        // GET: Warranties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warranty warranty = db.Warranty.Find(id);
            if (warranty == null)
            {
                return HttpNotFound();
            }
            return View(warranty);
        }

        // POST: Warranties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Warranty warranty = db.Warranty.Find(id);
            db.Warranty.Remove(warranty);
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
