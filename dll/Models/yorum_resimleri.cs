using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yorum_resimleri
    {
        public Nullable<int> yorum_id { get; set; }
        public int id { get; set; }
        public string resim_adi { get; set; }
        public virtual yorum yorum { get; set; }
    }
}
