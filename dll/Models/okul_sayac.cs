using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_sayac
    {
        public int okul_id { get; set; }
        public int sayac { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
