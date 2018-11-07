using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yetkiler
    {
        public yetkiler()
        {
            this.yoneticilers = new List<yoneticiler>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public string aciklama { get; set; }
        public string aciklama_ingilizce { get; set; }
        public Nullable<int> sira_no { get; set; }
        public string menu_degeri { get; set; }
        public string url { get; set; }
        public virtual ICollection<yoneticiler> yoneticilers { get; set; }
    }
}
