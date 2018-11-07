using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class kurumsal_yoneticiler
    {
        public kurumsal_yoneticiler()
        {
            this.kurumsal_yonetici_sube = new List<kurumsal_yonetici_sube>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string mail { get; set; }
        public string sifre { get; set; }
        public Nullable<int> okul_id { get; set; }
        public virtual ICollection<kurumsal_yonetici_sube> kurumsal_yonetici_sube { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
