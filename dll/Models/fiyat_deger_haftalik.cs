using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class fiyat_deger_haftalik
    {
        public int id { get; set; }
        public int fiyat_id { get; set; }
        public int fiyat_ogr_tur_id { get; set; }
        public Nullable<decimal> C1_hafta { get; set; }
        public Nullable<decimal> C2_hafta { get; set; }
        public Nullable<decimal> C3_hafta { get; set; }
        public Nullable<decimal> C4_hafta { get; set; }
        public Nullable<decimal> C6_hafta { get; set; }
        public Nullable<decimal> C8_hafta { get; set; }
        public Nullable<decimal> C10_hafta { get; set; }
        public Nullable<decimal> C12_hafta { get; set; }
        public Nullable<decimal> C24_hafta { get; set; }
        public Nullable<decimal> C36_hafta { get; set; }
        public virtual fiyat fiyat { get; set; }
        public virtual fiyat_ogr_tur fiyat_ogr_tur { get; set; }
    }
}
