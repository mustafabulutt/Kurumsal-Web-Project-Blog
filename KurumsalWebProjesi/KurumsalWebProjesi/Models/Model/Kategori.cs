using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [Required]
        public string KategoriAd { get; set; }

        public string KategoriAciklama { get; set; }

        public ICollection<Blog> blogs { get; set; }
    }
}