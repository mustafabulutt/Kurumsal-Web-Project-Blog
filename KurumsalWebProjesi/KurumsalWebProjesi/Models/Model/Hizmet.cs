using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Hizmet")]
    public class Hizmet
    {
        [Key]
        public int HizmetId { get; set; }
        [Required]
        [DisplayName("Baslik")]
        public string Baslik { get; set; }

        [Required]
        [DisplayName("Aciklama")]
        public string Aciklama { get; set; }

        [DisplayName("Resim")]
        public string ResimUrl { get; set; }
    }
}