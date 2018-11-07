using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yoneticiler
    {
        public yoneticiler()
        {
            this.yetkilers = new List<yetkiler>();
        }

        public int yonetici_id { get; set; }
        public string yonetici_adi { get; set; }
        public string yonetici_mail { get; set; }
        public string yonetici_sifre { get; set; }
        public string hatirlatma_sorusu { get; set; }
        public string hatirlatma_cevabi { get; set; }
        public string yonetici_foto { get; set; }
        public virtual ICollection<yetkiler> yetkilers { get; set; }
    }
}
