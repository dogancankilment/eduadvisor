using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yorum_sikayetleri
    {
        public int id { get; set; }
        public Nullable<int> yorum_id { get; set; }
        public Nullable<int> uye_id { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public string nedeni { get; set; }
        public byte incelendi { get; set; }
        public Nullable<System.DateTime> incelenme_tarihi { get; set; }
        public virtual uyeler uyeler { get; set; }
        public virtual yorum yorum { get; set; }
    }
}
