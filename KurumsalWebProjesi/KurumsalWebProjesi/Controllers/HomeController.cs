using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDbContext db = new KurumsalDbContext();
        // GET: Home
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList());
        }

        public ActionResult HizmetPartial()
        {
            

            return View(db.Hizmet.ToList());
        }

        public ActionResult SonBlogKayıtPartial()
        {

            var Liste = (from i in db.Blog
                         orderby i.BlogId descending /*blog listesinne ekelenen kayıtları orderbydesceding ile sondan başa doğru sıraladım ve anasayfada son eklenen postları gösterdim*/
                         select i).ToList();
        

            return View(Liste);
        }

        public ActionResult IletisimLayoutPartial()
        {

            return View(db.Iletisim.ToList());
        }

        public ActionResult BlogKategoriPartialLayout()
        {

            return View(db.Kategori.ToList()) ;
        }
           
        public ActionResult LayoutSosyalAgPartial()
        {
            return View(db.Iletisim.ToList());
        }

        public ActionResult Hakkimizda()
        {
            return View(db.Hakkimizda.SingleOrDefault());
        }

        public ActionResult Iletisim()
        {
            return View(db.Iletisim.ToList());
        }

        [HttpPost]
        public ActionResult Iletisim(string ad=null,string soyad=null,string email=null,string konu=null,string message= null)
        {
            if (ad!=null&&soyad!=null&&email!=null&&konu!=null&& message!= null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "webkurumsal16@gmail.com";
                WebMail.Password = "paraside16";
                WebMail.SmtpPort = 587;
                WebMail.Send("qspy147@gmail.com", konu, email + "&nbsp;" + message);

                ViewBag.uyari = "Mesajınız Başarılı bir şekilde gönderildi en kısa sürede sizinle iletişime geçilecektir";
            }
            else { 
            ViewBag.uyari = "Hata oluştu lütfen daha sonra tekrar deneyiniz";
            }
            return View(db.Iletisim.ToList()) ;
        }

        public ActionResult Hizmetlerimiz()
        {
            return View(db.Hizmet.ToList());
        }

        public ActionResult Blog(int? id)
        {

            if (id!=null)
            {
                var kat = db.Blog.Where(x => x.KategoriId == id).ToList();


                if (kat.Count==0)
                {
                    ViewBag.hata = "Aradığınız kategoride post bulunamadı";
                    return View(db.Blog.ToList()) ;

                }

                
                return View(kat);

            }


            return View(db.Blog.ToList()) ;
        }

        public ActionResult BlogDetay(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detay = db.Blog.Where(x => x.BlogId == id).ToList();

            if (detay == null)
            {
                return HttpNotFound();
            }

            return View(detay);
        }


        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
        {

            if (icerik == null)
            {
               
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, email = eposta, Icerik = icerik, BlogId = blogid, Onay = false });
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public  ActionResult KategorilerBlogDetayPartıal()
        {
            return View(db.Kategori.ToList());
        }

        public ActionResult LayoutKategoriPartial()
        {
            return View(db.Kategori.ToList());
        }

        public ActionResult BlogYorumlarıPartial(int id)
        {
            var y = db.Yorum.Where(x => x.BlogId == id).ToList();
            var onay = y.Where(x => x.Onay == true).ToList();

            return View(onay);
        }





    }
}