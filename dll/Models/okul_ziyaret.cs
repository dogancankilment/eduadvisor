using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_ziyaret
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public int uye_id { get; set; }
        public System.DateTime tarih { get; set; }
        public virtual okullar okullar { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
