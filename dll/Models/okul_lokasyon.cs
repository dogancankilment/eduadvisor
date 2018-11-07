using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_lokasyon
    {
        public int okul_id { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
