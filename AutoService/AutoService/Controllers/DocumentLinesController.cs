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
    public class DocumentLinesController : Controller
    {
        private AutoServiceDBEntities db = new AutoServiceDBEntities();

        // GET: DocumentLines
        public ActionResult Index()
        {
            var documentLine = db.DocumentLine.Include(d => d.DemandDocument).Include(d => d.Part);
            return View(documentLine.ToList());
        }

        // GET: DocumentLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentLine documentLine = db.DocumentLine.Find(id);
            if (documentLine == null)
            {
                return HttpNotFound();
            }
            return View(documentLine);
        }

        // GET: DocumentLines/Create
        public ActionResult Create()
        {
            ViewBag.DocumentID = new SelectList(db.DemandDocument, "DocumentID", "Status");
            ViewBag.PartID = new SelectList(db.Part, "PartID", "PartName");
            return View();
        }

        // POST: DocumentLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LineID,DocumentID,PartID,Quantity")] DocumentLine documentLine)
        {
            if (ModelState.IsValid)
            {
                db.DocumentLine.Add(documentLine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentID = new SelectList(db.DemandDocument, "DocumentID", "Status", documentLine.DocumentID);
            ViewBag.PartID = new SelectList(db.Part, "PartID", "PartName", documentLine.PartID);
            return View(documentLine);
        }

        // GET: DocumentLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentLine documentLine = db.DocumentLine.Find(id);
            if (documentLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentID = new SelectList(db.DemandDocument, "DocumentID", "Status", documentLine.DocumentID);
            ViewBag.PartID = new SelectList(db.Part, "PartID", "PartName", documentLine.PartID);
            return View(documentLine);
        }

        // POST: DocumentLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LineID,DocumentID,PartID,Quantity")] DocumentLine documentLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentID = new SelectList(db.DemandDocument, "DocumentID", "Status", documentLine.DocumentID);
            ViewBag.PartID = new SelectList(db.Part, "PartID", "PartName", documentLine.PartID);
            return View(documentLine);
        }

        // GET: DocumentLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentLine documentLine = db.DocumentLine.Find(id);
            if (documentLine == null)
            {
                return HttpNotFound();
            }
            return View(documentLine);
        }

        // POST: DocumentLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentLine documentLine = db.DocumentLine.Find(id);
            db.DocumentLine.Remove(documentLine);
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
