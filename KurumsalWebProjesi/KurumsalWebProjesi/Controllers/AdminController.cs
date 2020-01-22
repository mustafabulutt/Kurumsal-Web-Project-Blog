using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDbContext db = new KurumsalDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.yorumsayisi = db.Yorum.Count();
            ViewBag.blogpostt = db.Blog.Count();
            ViewBag.hizmett = db.Hizmet.Count();
            ViewBag.kategorilerr = db.Kategori.Count();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.AdminEposta == admin.AdminEposta).SingleOrDefault();
            if (login!=null)
            {

            
            if ( login.AdminEposta==admin.AdminEposta && login.Sifre==admin.Sifre)
            {
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.AdminEposta;
                    Session["adminyetki"] = login.Yetki;
                
                return RedirectToAction("Index","Admin");

            }

            }
            ViewBag.uyari = "Kullanıcı adı ve şifrenizi kontrol ederek tekrar deneyin";
            return View();
        }


     

        public ActionResult Logout()
        {
            Session["adminıd"] = null;
            Session["eposta"] = null;
            Session["adminyetki"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}