using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_sistem
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public string link { get; set; }
        public string kullanici_adi { get; set; }
        public string sifre { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
