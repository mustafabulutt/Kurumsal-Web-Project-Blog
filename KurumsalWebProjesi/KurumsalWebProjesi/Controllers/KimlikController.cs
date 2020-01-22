﻿using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalDbContext db = new KurumsalDbContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }

        

        
        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase LogoUrl)
        {

            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
                if (LogoUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.LogoUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.LogoUrl));
                    }
                    WebImage img = new WebImage(LogoUrl.InputStream);
                    FileInfo imginfo = new FileInfo(LogoUrl.FileName);
                    string logoname = LogoUrl.FileName+imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Upload/Kimlik/"+logoname);

                    k.LogoUrl = "/Upload/Kimlik/"+logoname;
                }
                k.Title = kimlik.Title;
                k.Keywords = kimlik.Keywords;
                k.Description = kimlik.Description;
                k.Unvan = kimlik.Unvan;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(kimlik);

        }


    }
}
