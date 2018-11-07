using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class program_havuzu
    {
        public program_havuzu()
        {
            this.okul_programlari = new List<okul_programlari>();
            this.on_kayit_basvuru = new List<on_kayit_basvuru>();
            this.uye_okullari = new List<uye_okullari>();
            this.yorums = new List<yorum>();
        }

        public int id { get; set; }
        public int egitim_id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ICollection<okul_programlari> okul_programlari { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public virtual ICollection<uye_okullari> uye_okullari { get; set; }
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
