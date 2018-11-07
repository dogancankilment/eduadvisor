using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class anket_cevaplayan
    {
        public anket_cevaplayan()
        {
            this.anket_cevaplari = new List<anket_cevaplari>();
        }

        public int id { get; set; }
        public Nullable<int> uye_id { get; set; }
        public Nullable<int> okul_id { get; set; }
        public Nullable<int> anket_id { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public virtual ICollection<anket_cevaplari> anket_cevaplari { get; set; }
        public virtual Anketler Anketler { get; set; }
        public virtual okullar okullar { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
