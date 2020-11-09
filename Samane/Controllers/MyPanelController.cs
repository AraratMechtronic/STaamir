using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Samane.DbContext_Classes;
using Samane.Models;

using Newtonsoft.Json;

namespace Samane.Controllers
{

    public class MyPanelController : Controller
    {
        private SamaneDbContext db = new SamaneDbContext();
        private ApplicationDbContext Userdb = new ApplicationDbContext();
        IndexForHospitalInfoViewModel indexForHospitalInfoViewModel;
        List<string> errorMessage = new List<string>();

        // GET: MyPanel
        public ActionResult Index()
        {

            var userforadmin = new ApplicationUser();
            var userforhospital = new ApplicationUserForHospitals();
            var userforengineer = new ApplicationUserForEngineers();

            indexForHospitalInfoViewModel = new IndexForHospitalInfoViewModel(User.Identity.Name);

            if (User.IsInRole("AdminUser"))
            {
                var adminUsers = Userdb.Users.OfType<ApplicationUser>().ToList();
                userforadmin = adminUsers.FirstOrDefault(m => m.UserName == User.Identity.Name);
                return View("IndexForAdminInfo", userforadmin);
            }
            else if (User.IsInRole("HospitalUsers"))
            {

                ///new code is written here
                ///in this code the Hospital information including instruments is loaded
                GetHospitalViewInformation();
                //pass through the view to show detail information
                return View("IndexForHospitalInfo", indexForHospitalInfoViewModel);

            }
            else if (User.IsInRole("EngineerUsers"))
            {
                var engineerUsers = Userdb.Users.OfType<ApplicationUserForEngineers>().ToList();
                userforengineer = engineerUsers.FirstOrDefault(m => m.UserName == User.Identity.Name);
                return View("IndexForEngineerInfo", userforengineer);
            }
            else
            {
                return Content("db.MyPanelViewModels.ToList()");
            }
        }

        // GET: MyPanel/FillHospitalInfo/5
        public ActionResult FillHospitalInfo(IndexForHospitalInfoViewModel indexForHospitalInfoViewModel)
        {
            return View(indexForHospitalInfoViewModel.hospital);
        }



        // POST: MyPanel/FillHospitalInfo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillHospitalInfo(Hospital hospital)
        {

            if (ModelState.IsValid)
            {
                var hospitalUsers = Userdb.Users.OfType<ApplicationUserForHospitals>().ToList();
                var x = hospitalUsers.SingleOrDefault(m => m.UserName == hospital.UserNamee);


                if (x.HospitalName != hospital.HospitalName || x.NameAndLastName != hospital.InChargePerson || x.PhoneNumber != hospital.PhoneNumber)
                {
                    x.HospitalName = hospital.HospitalName;
                    x.PhoneNumber = hospital.PhoneNumber;
                    x.NameAndLastName = hospital.InChargePerson;
                    Userdb.SaveChanges();
                }

                db.Hospitals.AddOrUpdate(hospital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hospital);
        }



        // GET: MyPanel/Create
        public ActionResult AddInstruments()
        {
            List<SelectListItem> hospitalNames = new List<SelectListItem>();
            var hospitalUsers = Userdb.Users.OfType<ApplicationUserForHospitals>().ToList();
            var userforhospital = hospitalUsers.FirstOrDefault(m => m.UserName == User.Identity.Name);
            hospitalNames.Add(new SelectListItem() { Value = userforhospital.HospitalName, Text = userforhospital.HospitalName });
            //foreach (var x in db.Hospitals)
            //{
            //    hospitalNames.Add(new SelectListItem() { Value = x.HospitalName, Text = x.HospitalName });

            //}
            var instrumet = new Instrument()
            {
                UserName = userforhospital.UserName
            };
            ViewBag.hospitalNamess = hospitalNames;
            return PartialView("_ModalInstrument", instrumet);
        }

        //// POST: MyPanel/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddInstruments(Instrument instrumentmodel)
        //{
        //    if (!ModelState.IsValid)
        //        return PartialView("_ModalInstrument", instrumentmodel);
        //    if (ModelState.IsValid)
        //    {
        //        db.Instruments.AddOrUpdate(instrumentmodel);
        //        if (db.SaveChanges() <= 0)
        //            return PartialView("_ModalInstrument", instrumentmodel);
        //    }
        //    //return JavaScript("window.close();");
        //    GetHospitalViewInformation();
        //    return RedirectToAction("Index");
        //}
        public JsonResult InstrumentAdd(Instrument thisInstrument)
        {
            #region Checking Model state

            ModelState.Remove("thisInstrument.UserName");
            ModelState.Remove("thisInstrument.hospital");
            if (!ModelState.IsValid)
            {
                string errorMessage = "<li>" + string.Join("<li>", ModelState.Values.Where(v => v.Errors.Count > 0).Select(e => e.Errors.First().ErrorMessage).ToList().ToArray());
                errorMessage += "</li>";
                return Json(new { success = false, responseText = errorMessage }, JsonRequestBehavior.AllowGet);
            }
            #endregion Checking Model state

            GetHospitalViewInformation();
            thisInstrument.UserName = indexForHospitalInfoViewModel.HospitalUser.UserName;

            if (ModelState.IsValid)
            {
                db.Instruments.AddOrUpdate(thisInstrument);
                if (db.SaveChanges() <= 0)
                    return Json(new { success = false, responseText = "Failed." }, JsonRequestBehavior.AllowGet);

            }
            //return JavaScript("window.close();");
            GetHospitalViewInformation();
            return Json(new
            {
                success = true
                            ,
                //responseText = indexForHospitalInfoViewModel.hospital.instruments.Count()
                //            ,
                responseText = indexForHospitalInfoViewModel.hospital.instruments.Last().InstrumentId
            }
                       , JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditHopitalInfo()
        {
            GetHospitalViewInformation();
            //opening Modal of Hospital user Editing
            return PartialView("_EditHospitalInfo", indexForHospitalInfoViewModel.hospital);
        }

        [HttpPost]
        public JsonResult EditHopitalInfo(Hospital thishospital)
        {
            #region Checking Model state

            ModelState.Remove("thishospital.userNamee");
            ModelState.Remove("thishospital.instruments");
            ModelState.Remove("thishospital.Province");
            ModelState.Remove("thishospital.City");
            if (!ModelState.IsValid)
            {
                string errorMessage = "<li>" + string.Join("<li>", ModelState.Values.Where(v => v.Errors.Count > 0).Select(e => e.Errors.First().ErrorMessage).ToList().ToArray());
                errorMessage += "</li>";
                return Json(new { success = false, responseText = errorMessage }, JsonRequestBehavior.AllowGet);
            }
            #endregion Checking Model state

            GetHospitalViewInformation();
            thishospital.UserNamee = indexForHospitalInfoViewModel.hospital.UserNamee;
            thishospital.City = indexForHospitalInfoViewModel.hospital.City;
            thishospital.Province = indexForHospitalInfoViewModel.hospital.Province;

            if (ModelState.IsValid)
            {
                db.Hospitals.AddOrUpdate(thishospital);
                //if save fail
                int result = db.SaveChanges();
                if (result < 0)
                    return Json(new { success = false, responseText = "خطا در ثبت اطلاعات." }, JsonRequestBehavior.AllowGet);
                else if (result == 0)
                    return Json(new { success = false, responseText = "تغییری در اطلاعات داده نشده است." }, JsonRequestBehavior.AllowGet);
            }
            //if saved successfully
            //GetHospitalViewInformation();
            return Json(new { success = true, responseText = "T" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditInstrument(int instrumentId)
        {
            GetHospitalViewInformation();
            var instrument = indexForHospitalInfoViewModel.hospital.instruments.Where(i => i.InstrumentId == instrumentId).FirstOrDefault();
            return PartialView("_EditInstrument", instrument);
        }

        [HttpPost]
        public ActionResult EditInstrument(Instrument instrumentModel)
        {

            GetHospitalViewInformation();
            //instrumentModel.hospital = indexForHospitalInfoViewModel.hospita`l;
            ModelState.Remove("hospital");
            List<string> errorMessage = new List<string>();
            if (!ModelState.IsValid)
                return PartialView("_EditInstrument", instrumentModel);

            //Updating hospitalInfo
            if (indexForHospitalInfoViewModel.EditInstrument(instrumentModel, out errorMessage))
            {
                GetHospitalViewInformation();
                return RedirectToAction("Index");//View("IndexForHospitalInfo",indexForHospitalInfoViewModel);
            }
            //return JavaScript("window.close();");
            return JavaScript("alert(" + errorMessage.First() + ");");

        }
        //public ActionResult DeleteInstrument(int insrumentId)
        //{
        //    errorMessage = new List<string>();
        //    indexForHospitalInfoViewModel.DeleteInstrument(insrumentId, out errorMessage);
        //    return RedirectToAction("Index");
        //}

        public JsonResult DeleteInstrument(int insrumentId)
        {
            GetHospitalViewInformation();

            errorMessage = new List<string>();
            if (indexForHospitalInfoViewModel.DeleteInstrument(insrumentId, out errorMessage))
                return Json(new { success = true, responseText = "حذف انجام شد" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, responseText = "تغییری در اطلاعات داده نشده است." }, JsonRequestBehavior.AllowGet);
        }
        public void GetHospitalViewInformation()
        {
            //in this object generation we initiate object with hospital information
            indexForHospitalInfoViewModel = new IndexForHospitalInfoViewModel(User.Identity.Name);

        }

        public JsonResult GetHospitalInstrument(int instrumentId)
        {
            if (indexForHospitalInfoViewModel is null)
            {
                GetHospitalViewInformation();
            }
            Instrument inst = indexForHospitalInfoViewModel.hospital.instruments.Where(i => i.InstrumentId == 86).First();
            inst.hospital = null;
            string _responseText = JsonConvert.SerializeObject(inst, Formatting.Indented);
            return Json(new { success = true, responseText = _responseText }, JsonRequestBehavior.AllowGet);
            
        }
        // GET: MyPanel/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MyPanelViewModel myPanelViewModel = db.MyPanelViewModels.Find(id);
        //    if (myPanelViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(myPanelViewModel);
        //}

        // GET: MyPanel/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: MyPanel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id")] MyPanelViewModel myPanelViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MyPanelViewModels.Add(myPanelViewModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(myPanelViewModel);
        //}

        // GET: MyPanel/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MyPanelViewModel myPanelViewModel = db.MyPanelViewModels.Find(id);
        //    if (myPanelViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(myPanelViewModel);
        //}

        // POST: MyPanel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id")] MyPanelViewModel myPanelViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(myPanelViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(myPanelViewModel);
        //}

        // GET: MyPanel/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MyPanelViewModel myPanelViewModel = db.MyPanelViewModels.Find(id);
        //    if (myPanelViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(myPanelViewModel);
        //}

        // POST: MyPanel/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MyPanelViewModel myPanelViewModel = db.MyPanelViewModels.Find(id);
        //    db.MyPanelViewModels.Remove(myPanelViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // Renders the partial view which will be shown in a modal


        // Renders the partial view which will be shown in a modal

        //protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new JsonResult()
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior,
        //        MaxJsonLength = Int32.MaxValue
        //    };
        //}
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
