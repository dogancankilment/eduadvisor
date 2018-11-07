using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_fotograflar
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public string resim_adi { get; set; }
        public Nullable<int> sira_no { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
