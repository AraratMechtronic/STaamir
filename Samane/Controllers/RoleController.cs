using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Samane.DbContext_Classes;
using Samane.Models;

namespace Samane.Controllers
{
    [AllowAnonymous()]
    public class RoleController : Controller
    {
        private SamaneDbContext db = new SamaneDbContext();
        private ApplicationRoleManager _roleManager;


        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            this.RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> List = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                List.Add(new RoleViewModel(role));
            }
            return View(List);
        }

        // GET: Role/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(new RoleViewModel(role));
            }
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole() { Name = model.Name };
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Role/Edit/5
        public async Task<ActionResult> Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(new RoleViewModel(role));
            }
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Role/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                await RoleManager.DeleteAsync(role);
                return RedirectToAction("Index");
            }
        }

        // POST: Role/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    RoleViewModel roleViewModel = db.RoleViewModels.Find(id);
        //    db.RoleViewModels.Remove(roleViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
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
