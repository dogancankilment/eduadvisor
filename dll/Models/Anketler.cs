using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class Anketler
    {
        public Anketler()
        {
            this.anket_cevaplayan = new List<anket_cevaplayan>();
            this.Anket_Sorulari = new List<Anket_Sorulari>();
        }

        public int id { get; set; }
        public int egitim_id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public byte aktif { get; set; }
        public Nullable<byte> silindi { get; set; }
        public virtual ICollection<anket_cevaplayan> anket_cevaplayan { get; set; }
        public virtual ICollection<Anket_Sorulari> Anket_Sorulari { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
    }
}
