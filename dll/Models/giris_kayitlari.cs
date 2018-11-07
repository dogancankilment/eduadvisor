using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class giris_kayitlari
    {
        public System.DateTime tarih { get; set; }
        public string ip_no { get; set; }
        public Nullable<int> yonetici_id { get; set; }
        public Nullable<System.DateTime> cikis_tarih { get; set; }
    }
}
