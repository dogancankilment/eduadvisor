using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class mesajlar
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string mail { get; set; }
        public string tel { get; set; }
        public string ip { get; set; }
        public string mesaj { get; set; }
        public string konu { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
    }
}
