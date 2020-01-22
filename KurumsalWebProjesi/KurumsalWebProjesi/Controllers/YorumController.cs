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
    public class YorumController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();

        // GET: Yorum
        public ActionResult Index()
        {
            var yorum = db.Yorum.Include(y => y.blog);
            return View(yorum.ToList());
        }

     

       

        // POST: Yorum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
     

        // GET: Yorum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Blog, "BlogId", "Baslik", yorum.BlogId);
            return View(yorum);
        }

        // POST: Yorum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YorumId,AdSoyad,email,Icerik,Onay,BlogId")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.Blog, "BlogId", "Baslik", yorum.BlogId);
            return View(yorum);
        }

        // GET: Yorum/Delete/5
      

        // POST: Yorum/Delete/5
       
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum = db.Yorum.Find(id);

            if (yorum == null)
            {
                return HttpNotFound();
            }

            db.Yorum.Remove(yorum);
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

        public ActionResult YorumBildirimPartial()
        {
            

            ViewBag.yorum = db.Yorum.Where(x => x.Onay == false).Count();

            return View();
        }
    }
}
