using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class uye_okullari
    {
        public int id { get; set; }
        public int uye_id { get; set; }
        public int okul_id { get; set; }
        public int program_id { get; set; }
        public int program_tur_id { get; set; }
        public string baslangic { get; set; }
        public string bitis { get; set; }
        public System.DateTime eklenme_tarihi { get; set; }
        public virtual okullar okullar { get; set; }
        public virtual onkayit_egitim_turleri onkayit_egitim_turleri { get; set; }
        public virtual program_havuzu program_havuzu { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
