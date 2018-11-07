using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class fiyat
    {
        public fiyat()
        {
            this.fiyat_deger = new List<fiyat_deger>();
            this.fiyat_deger_haftalik = new List<fiyat_deger_haftalik>();
        }

        public int id { get; set; }
        public int okul_program_id { get; set; }
        public int fiyat_tur_id { get; set; }
        public Nullable<int> okul_id { get; set; }
        public virtual ICollection<fiyat_deger> fiyat_deger { get; set; }
        public virtual ICollection<fiyat_deger_haftalik> fiyat_deger_haftalik { get; set; }
        public virtual fiyat_tur fiyat_tur { get; set; }
        public virtual okul_programlari okul_programlari { get; set; }
        public virtual okullar okullar { get; set; }
    }
}
