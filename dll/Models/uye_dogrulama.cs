using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class uye_dogrulama
    {
        public int uye_id { get; set; }
        public string dogrulama_kodu { get; set; }
        public int onay { get; set; }
        public Nullable<System.DateTime> dogrulama_tarihi { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
