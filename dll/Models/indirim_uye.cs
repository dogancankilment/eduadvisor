using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class indirim_uye
    {
        public int id { get; set; }
        public Nullable<int> mesaj_id { get; set; }
        public Nullable<int> uye_id { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public Nullable<byte> durumu { get; set; }
        public virtual indirim_uye_bilgilendirme indirim_uye_bilgilendirme { get; set; }
        public virtual toplu_mesaj toplu_mesaj { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
