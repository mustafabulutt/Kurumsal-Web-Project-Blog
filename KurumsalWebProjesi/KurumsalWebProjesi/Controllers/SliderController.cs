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
    public class SliderController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

     
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id ,Slider slider,HttpPostedFileBase ResimUrl)
        {
            if (ModelState.IsValid)
            {
                var s = db.Slider.Where(x => x.SliderId == id).SingleOrDefault();

                if (ResimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.ResimUrl));
                    }
                    WebImage img = new WebImage(ResimUrl.InputStream);
                    FileInfo imginfo = new FileInfo(ResimUrl.FileName);
                    string logoname = ResimUrl.FileName + imginfo.Extension;
                    
                    img.Save("~/Upload/AnaSayfaResim/" + logoname);

                    s.ResimUrl = "/Upload/AnaSayfaResim/" + logoname;
                }
                s.Acıklama = slider.Acıklama;
                s.Baslik = slider.Baslik;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
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
