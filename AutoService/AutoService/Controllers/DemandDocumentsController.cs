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
    public class DemandDocumentsController : Controller
    {
        private AutoServiceDBEntities db = new AutoServiceDBEntities();

        // GET: DemandDocuments
        public ActionResult Index()
        {
            var demandDocument = db.DemandDocument.Include(d => d.Repair);
            return View(demandDocument.ToList());
        }

        // GET: DemandDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandDocument demandDocument = db.DemandDocument.Find(id);
            if (demandDocument == null)
            {
                return HttpNotFound();
            }
            return View(demandDocument);
        }

        // GET: DemandDocuments/Create
        public ActionResult Create()
        {
            ViewBag.RepairID = new SelectList(db.Repair, "RepairID", "Status");
            return View();
        }

        // POST: DemandDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentID,RepairID,Date,Status")] DemandDocument demandDocument)
        {
            if (ModelState.IsValid)
            {
                db.DemandDocument.Add(demandDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RepairID = new SelectList(db.Repair, "RepairID", "Status", demandDocument.RepairID);
            return View(demandDocument);
        }

        // GET: DemandDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandDocument demandDocument = db.DemandDocument.Find(id);
            if (demandDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.RepairID = new SelectList(db.Repair, "RepairID", "Status", demandDocument.RepairID);
            return View(demandDocument);
        }

        // POST: DemandDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentID,RepairID,Date,Status")] DemandDocument demandDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demandDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RepairID = new SelectList(db.Repair, "RepairID", "Status", demandDocument.RepairID);
            return View(demandDocument);
        }

        // GET: DemandDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandDocument demandDocument = db.DemandDocument.Find(id);
            if (demandDocument == null)
            {
                return HttpNotFound();
            }
            return View(demandDocument);
        }

        // POST: DemandDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DemandDocument demandDocument = db.DemandDocument.Find(id);
            db.DemandDocument.Remove(demandDocument);
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
