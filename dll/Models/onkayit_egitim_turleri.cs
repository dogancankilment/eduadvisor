using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class onkayit_egitim_turleri
    {
        public onkayit_egitim_turleri()
        {
            this.on_kayit_basvuru = new List<on_kayit_basvuru>();
            this.uye_okullari = new List<uye_okullari>();
            this.yorums = new List<yorum>();
        }

        public int id { get; set; }
        public int egitim_id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public int sira_no { get; set; }
        public byte silindi { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public virtual ICollection<uye_okullari> uye_okullari { get; set; }
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
