using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_fiyat_ogr_tur
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public int fiyat_ogr_tur_id { get; set; }
        public virtual fiyat_ogr_tur fiyat_ogr_tur { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
