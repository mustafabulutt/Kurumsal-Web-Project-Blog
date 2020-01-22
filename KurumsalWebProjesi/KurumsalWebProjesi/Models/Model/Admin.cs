using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required,StringLength(50,ErrorMessage ="Maksimum 50 karakter!")]
        public string AdminEposta { get; set; }
        [Required,StringLength(50,ErrorMessage ="maksimum 50 karakter!")]
        public string Sifre { get; set; }
        public string Yetki { get; set; }
    }
}