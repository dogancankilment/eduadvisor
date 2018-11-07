using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okul_grup_iliski
    {
        public int id { get; set; }
        public int okul_id { get; set; }
        public Nullable<int> grup_id { get; set; }
        public virtual okul_gruplari okul_gruplari { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
