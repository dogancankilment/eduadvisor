using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class ilce
    {
        public int id { get; set; }
        public string adi { get; set; }
        public int il_id { get; set; }
        public virtual il il { get; set; }
    }
}
