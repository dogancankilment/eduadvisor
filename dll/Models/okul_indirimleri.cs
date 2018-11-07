using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_indirimleri
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public int orani { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
