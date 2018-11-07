using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yorum_begeniler
    {
        public int id { get; set; }
        public int yorum_id { get; set; }
        public int uye_id { get; set; }
        public virtual uyeler uyeler { get; set; }
        public virtual yorum yorum { get; set; }
    }
}
