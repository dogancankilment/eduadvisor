using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class indirim_uye_bilgilendirme
    {
        public int indirim_uye_id { get; set; }
        public Nullable<byte> durumu { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public virtual indirim_uye indirim_uye { get; set; }
    }
}
