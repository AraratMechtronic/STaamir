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
    public class HospitalController : Controller
    {
        private SamaneDbContext db = new SamaneDbContext();
        private SamaneDbContext SamanehDB = new SamaneDbContext();
        // GET: Hospitals
        public ActionResult Index()
        {


            //db.Hospitals.Include(t => t.instruments);
            return View(db.Hospitals.ToList());
            
            
        }

        // GET: Hospitals/Details/5
        public ActionResult Details(string hospitalName)
        {
            if (hospitalName == null)
            {
                return View("index", db.Hospitals.ToList());
            }

            var hospitalll = db.Hospitals.Include(a => a.instruments).SingleOrDefault(b => b.HospitalName== hospitalName);
            if (hospitalll == null)
                return HttpNotFound();
            else
                return View(hospitalll.instruments);


            //Hospital hospitall = db.Hospitals.Find(id);
            //if (hospitall == null)
            //{
            //    return HttpNotFound();
            //}
            //else
            //{
            //    var x = new List<Instrument>();
            //    foreach (var t in db.Instruments)
            //    {
            //        if (t.HospitalName==hospitall.HospitalName)
            //        {
            //            x.Add(t);
            //        }
            //    }

            //    return View(hospitall);
            //}

        }

        // GET: Hospitals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HospitalName,Province,City,InChargePerson,PhoneNumber")] Hospital hospital)
        {
            hospital.UserNamee = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Hospitals.Add(hospital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public ActionResult Edit(string hospitalName)
        {
            if (hospitalName == null)
            {
                return View("index", db.Hospitals.ToList());
            }

            Hospital hospital = db.Hospitals.Where(hos => hos.HospitalName.Contains(hospitalName)).FirstOrDefault();
            
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "HospitalName,Province,City,InChargePerson,PhoneNumber")]
        public ActionResult Edit( Hospital hospital)
        {
            //hospital.UserNamee = User.Identity.Name;      
            //hospital.instruments= db.Hospitals.Include(m => m.instruments).FirstOrDefault(m => (m.UserNamee == hospital.UserNamee)).instruments;
            if (ModelState.IsValid)
            {
                db.Entry(hospital).State = EntityState.Modified;
                if(db.SaveChanges()>=0)
                    return RedirectToAction("Index");
                return View(hospital);
            }
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        [Route("Hospital/Delete/{hospitalName}/{city}")]
        public ActionResult Delete(string hospitalName, string city)
        {
            if (hospitalName == null || city == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospital hospital = db.Hospitals.Include(m => m.instruments).FirstOrDefault(m => (m.HospitalName == hospitalName && m.City == city)); 
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [Route("Hospital/Delete/{hospitalName}/{city}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string hospitalName, string city)
        {
            Hospital hospital = db.Hospitals.Include(m =>m.instruments).FirstOrDefault(m => (m.HospitalName == hospitalName && m.City == city));

            db.Hospitals.Remove(hospital);
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
