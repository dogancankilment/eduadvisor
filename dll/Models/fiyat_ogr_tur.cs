using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class fiyat_ogr_tur
    {
        public fiyat_ogr_tur()
        {
            this.fiyat_deger = new List<fiyat_deger>();
            this.fiyat_deger_haftalik = new List<fiyat_deger_haftalik>();
            this.okul_fiyat_ogr_tur = new List<okul_fiyat_ogr_tur>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ing { get; set; }
        public virtual ICollection<fiyat_deger> fiyat_deger { get; set; }
        public virtual ICollection<fiyat_deger_haftalik> fiyat_deger_haftalik { get; set; }
        public virtual ICollection<okul_fiyat_ogr_tur> okul_fiyat_ogr_tur { get; set; }
    }
}
