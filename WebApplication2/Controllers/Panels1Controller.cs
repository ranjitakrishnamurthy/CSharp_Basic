using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class Panels1Controller : Controller
    {
        private PanelDataContext db = new PanelDataContext();

        // GET: Panels1
        public ActionResult Index()
        {
            return View(db.Panels.ToList());
        }

        // GET: Panels1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel panel = db.Panels.Find(id);
            if (panel == null)
            {
                return HttpNotFound();
            }
            return View(panel);
        }

        // GET: Panels1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panels1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Category,Price")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                db.Panels.Add(panel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(panel);
        }

        // GET: Panels1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel panel = db.Panels.Find(id);
            if (panel == null)
            {
                return HttpNotFound();
            }
            return View(panel);
        }

        // POST: Panels1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Category,Price")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(panel);
        }

        // GET: Panels1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel panel = db.Panels.Find(id);
            if (panel == null)
            {
                return HttpNotFound();
            }
            return View(panel);
        }

        // POST: Panels1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Panel panel = db.Panels.Find(id);
            db.Panels.Remove(panel);
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
