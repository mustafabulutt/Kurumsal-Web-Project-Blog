using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Yorum")]
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }


        [DisplayName("Ad Soyad")]
        [Required,StringLength(50,ErrorMessage ="50 karakter olabilir")]
        public string AdSoyad { get; set; }

        [Required, StringLength(30, ErrorMessage = "30 karakter olabilir")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required, StringLength(150, ErrorMessage = "15050 karakter olabilir")]
        [DisplayName("Yorumunuz")]
        public string Icerik { get; set; }

        public bool Onay { get; set; }

        public int? BlogId { get; set; }

        public Blog blog { get; set; }



    }
}