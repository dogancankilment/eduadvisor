using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class Anket_Sorulari
    {
        public Anket_Sorulari()
        {
            this.anket_cevaplari = new List<anket_cevaplari>();
        }

        public int id { get; set; }
        public Nullable<int> anket_id { get; set; }
        public string soru { get; set; }
        public string soru_ingilizce { get; set; }
        public Nullable<int> sira_no { get; set; }
        public Nullable<byte> silindi { get; set; }
        public virtual ICollection<anket_cevaplari> anket_cevaplari { get; set; }
        public virtual Anketler Anketler { get; set; }
    }
}
