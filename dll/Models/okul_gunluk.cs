using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_gunluk
    {
        public string ip { get; set; }
        public System.DateTime zaman { get; set; }
        public int okul_id { get; set; }
        public int gunluk_id { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
