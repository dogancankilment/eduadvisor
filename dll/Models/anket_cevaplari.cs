using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class anket_cevaplari
    {
        public Nullable<int> soru_id { get; set; }
        public Nullable<int> cevap_id { get; set; }
        public Nullable<int> degeri { get; set; }
        public int anket_cevap_id { get; set; }
        public virtual anket_cevaplayan anket_cevaplayan { get; set; }
        public virtual Anket_Sorulari Anket_Sorulari { get; set; }
    }
}
