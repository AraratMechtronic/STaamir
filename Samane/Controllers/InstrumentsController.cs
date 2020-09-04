using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Samane.DbContext_Classes;
using Samane.Models;

namespace Samane.Controllers
{
    [Authorize(Roles = "AdminUser")]
    public class InstrumentsController : Controller
    {
        private SamaneDbContext db = new SamaneDbContext();

        // GET: Instruments
        public ActionResult Index()
        {
            var instruments = db.Instruments.Include(i => i.hospital);
            return View(instruments.ToList());
        }

        // GET: Instruments/Details/5
        public ActionResult Details(int? id)
        {
            var instrument = db.Instruments.Include(a => a.hospital).SingleOrDefault(t => t.InstrumentId==id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Instrument instrument = instruments.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // GET: Instruments/Create
        public ActionResult Create()
        {
            ViewBag.HospitalName = new SelectList(db.Hospitals, "HospitalName", "Province");
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstrumentId,Category,Model,SerialNo,HospitalName")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                db.Instruments.Add(instrument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HospitalName = new SelectList(db.Hospitals, "HospitalName", "Province", instrument.UserName);
            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instrument = db.Instruments.Include(m => m.hospital).FirstOrDefault(n => n.InstrumentId == id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            ViewBag.HospitalName = new SelectList(db.Hospitals, "HospitalName", "Province", instrument.UserName);
            return View(instrument);
        }

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstrumentId,Category,Model,SerialNo")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                var instrumentt = db.Instruments.Include(m => m.hospital).FirstOrDefault(n=>n.InstrumentId==instrument.InstrumentId);
                instrumentt.Category = instrument.Category;
                instrumentt.Model = instrument.Model;
                instrumentt.SerialNo = instrument.SerialNo;
                db.SaveChanges();

                //db.Entry(instrumentt.hospital).State = EntityState.Modified;

                return RedirectToAction("Index");
            }
            ViewBag.HospitalName = new SelectList(db.Hospitals, "HospitalName", "Province", instrument.UserName);
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instrument = db.Instruments.Include(m => m.hospital).FirstOrDefault(n=>n.InstrumentId==id);         
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instrument instrument = db.Instruments.Find(id);
            db.Instruments.Remove(instrument);
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
