using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("iletişim")]
    public class Iletisim
    {
        [Key]
        public int IletisimId { get; set; }
        [Required]
        [DisplayName("Adres")]
        public string Adres { get; set; }

        [Required]
        [DisplayName("Telefon")]
        public string Telefon { get; set; }

        [Required]
        [DisplayName("Fax")]
        public string Fax { get; set; }

        [Required]
        [DisplayName("Whatsapp")]
        public string Whatsapp { get; set; }

        [Required]
        [DisplayName("Facebook")]
        public string Facebook { get; set; }

        [Required]
        [DisplayName("Twitter")]
        public string Twitter { get; set; }

        [Required]
        [DisplayName("Instagram")]
        public string Instagram { get; set; }

    }
}