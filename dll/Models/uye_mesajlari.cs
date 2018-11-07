using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class uye_mesajlari
    {
        public int id { get; set; }
        public int uye_id { get; set; }
        public string icerik { get; set; }
        public System.DateTime tarih { get; set; }
        public Nullable<byte> okundu { get; set; }
        public Nullable<byte> alici { get; set; }
        public decimal turu { get; set; }
    }
}
