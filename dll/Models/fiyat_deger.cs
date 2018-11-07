using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class fiyat_deger
    {
        public int id { get; set; }
        public int fiyat_id { get; set; }
        public int fiyat_ogr_tur_id { get; set; }
        public Nullable<decimal> deger { get; set; }
        public virtual fiyat fiyat { get; set; }
        public virtual fiyat_ogr_tur fiyat_ogr_tur { get; set; }
    }
}
