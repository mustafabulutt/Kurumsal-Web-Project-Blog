using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [DisplayName("Slider Görsel")]
        public string ResimUrl { get; set; }

        [DisplayName("Slider Başlık")]
        public string Baslik { get; set; }

        [DisplayName("Slider Açıklama")]
        public string Acıklama { get; set; }
    }
}