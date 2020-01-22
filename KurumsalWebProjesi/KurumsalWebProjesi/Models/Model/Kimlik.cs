using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
 { 
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int KimlikId { get; set; }

        [DisplayName("Site Başlık")]
        [Required,MaxLength(100,ErrorMessage =" Maksimum 100 karakter!")]
        public string Title { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        [Required, MaxLength(100, ErrorMessage = " Maksimum 100 karakter!")]
        public string Keywords { get; set; }

        [DisplayName("Site Açıklama")]
        [Required, MaxLength(100, ErrorMessage = " Maksimum 100 karakter!")]
        public string Description { get; set; }

        [DisplayName("Site Logo")]
        public string  LogoUrl { get; set; }
        [DisplayName("Site Unvan")]
        public string  Unvan { get; set; }

    }
}