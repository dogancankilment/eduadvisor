using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yorum_begeni_okul
    {
        public int yorum_id { get; set; }
        public int okul_id { get; set; }
        public virtual yorum yorum { get; set; }
    }
}
