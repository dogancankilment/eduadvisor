using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class kurumsal_yonetici_sube
    {
        public int id { get; set; }
        public int yonetici_id { get; set; }
        public int okul_id { get; set; }
        public virtual kurumsal_yoneticiler kurumsal_yoneticiler { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
