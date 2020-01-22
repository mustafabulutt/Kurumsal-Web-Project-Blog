using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalDbContext db = new KurumsalDbContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var h = db.Hakkimizda.ToList();
            return View(h);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).FirstOrDefault();
            if (h == null)
            {
                return HttpNotFound();
            }
            
            return View(h);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Hakkimizda h)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();
                hakkimizda.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(h);
        }
    }
}