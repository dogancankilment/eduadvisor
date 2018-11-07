using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class uye_ilgi
    {
        public int id { get; set; }
        public int uye_id { get; set; }
        public int egitim_id { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
