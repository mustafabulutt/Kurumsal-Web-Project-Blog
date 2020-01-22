using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class BlogController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();
        
        // GET: Blog
        public ActionResult Index()
        {


            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x=>x.BlogId));

      
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd"); 
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create( Blog blog ,HttpPostedFileBase ResimUrl)
        {

            if (ResimUrl != null)
            {
               
                WebImage img = new WebImage(ResimUrl.InputStream);
                FileInfo imginfo = new FileInfo(ResimUrl.FileName);
                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Upload/BlogImage/" + blogimgname);

                blog.ResimUrl = "/Upload/BlogImage/" + blogimgname;
            }
            db.Blog.Add(blog);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {

            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
            if (blog==null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd",blog.KategoriId);

            return View(blog);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Blog blog, HttpPostedFileBase ResimUrl)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
                if (ResimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimUrl));
                    }
                    WebImage img = new WebImage(ResimUrl.InputStream);
                    FileInfo imginfo = new FileInfo(ResimUrl.FileName);
                    string Blogimgname = ResimUrl.FileName + imginfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Upload/BlogImage/" + Blogimgname);

                    b.ResimUrl = "/Upload/BlogImage/" + Blogimgname;
                }
                b.Baslik = blog.Baslik;
                b.Icerik = blog.Icerik;
                b.KategoriId = blog.KategoriId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();

        }

        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd", blog.KategoriId);
            return View(blog);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();

            if (System.IO.File.Exists(Server.MapPath(b.ResimUrl)))
            {
                System.IO.File.Delete(Server.MapPath(b.ResimUrl));
            }

            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      


    }
}