using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_fakulteleri
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public int fakulte_id { get; set; }
        public virtual fakulte_havuzu fakulte_havuzu { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
