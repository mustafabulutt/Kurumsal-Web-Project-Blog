﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesi.Models.Model
{
    [Table("Blog")]
    public class Blog
    {

        public int BlogId { get; set; }
        public string Baslik { get; set; }

        public string Icerik { get; set; }

        public string ResimUrl { get; set; }

        public int? KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        public ICollection<Yorum> Yorum { get; set; }

    }
}