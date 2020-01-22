using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;

namespace KurumsalWebProjesi.Controllers
{
    public class HizmetsController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();

        // GET: Hizmets
        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create( Hizmet hizmet ,HttpPostedFileBase ResimUrl )
        {
            if (ModelState.IsValid)
            {
                if (ResimUrl != null)
                {
                    WebImage img = new WebImage(ResimUrl.InputStream);
                    FileInfo imginfo = new FileInfo(ResimUrl.FileName);
                    string logoname = ResimUrl.FileName + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Upload/Hizmet/" + logoname);

                    hizmet.ResimUrl = "/Upload/Hizmet/" + logoname;
                }

                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
                  
            }
            return View(hizmet);
        }
               
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmet hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hizmet);
        }

        public ActionResult Delete(int id)
        {
           
            if (id == null)
            {
                return HttpNotFound();
            }
            var h = db.Hizmet.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }


            System.IO.File.Delete(Server.MapPath(h.ResimUrl));            
            db.Hizmet.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hizmet hizmet, HttpPostedFileBase ResimUrl)
        {

            if (ModelState.IsValid)
            {
                var k = db.Hizmet.Where(x => x.HizmetId == id).SingleOrDefault();
                if (ResimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.ResimUrl));
                    }
                    WebImage img = new WebImage(ResimUrl.InputStream);
                    FileInfo imginfo = new FileInfo(ResimUrl.FileName);
                    string logoname = ResimUrl.FileName + imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Upload/Hizmet/" + logoname);

                    k.ResimUrl = "/Upload/Hizmet/" + logoname;
                }
                k.Baslik = hizmet.Baslik;
                k.Aciklama = hizmet.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(hizmet);

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
