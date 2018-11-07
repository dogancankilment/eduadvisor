using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yorum_yanitlari
    {
        public int yorum_id { get; set; }
        public string yanit { get; set; }
        public int onay { get; set; }
        public Nullable<System.DateTime> onay_tarihi { get; set; }
        public Nullable<System.DateTime> yanit_tarihi { get; set; }
        public virtual yorum yorum { get; set; }
    }
}
