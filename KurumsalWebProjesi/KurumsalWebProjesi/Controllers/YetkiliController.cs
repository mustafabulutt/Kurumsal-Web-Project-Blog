using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;

namespace KurumsalWebProjesi.Controllers
{
    public class YetkiliController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();

        // GET: Yetkili
        public ActionResult Index()
        {
            
            return View(db.Admin.ToList());
        }

        // GET: Yetkili/Details/5
       

        // GET: Yetkili/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yetkili/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin,string yetki)
        {
            if (ModelState.IsValid)
            {
                admin.Yetki = yetki;

                db.Admin.Add(admin);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Yetkili/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Yetkili/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,AdminEposta,Sifre,Yetki")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

       

     
        public ActionResult Delete(int id)
        {
            if (id!=1)
            {
                Admin admin = db.Admin.Find(id);
                db.Admin.Remove(admin);
                db.SaveChanges();
            }

            
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
