using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class toplu_mesaj
    {
        public toplu_mesaj()
        {
            this.indirim_uye = new List<indirim_uye>();
            this.toplu_mesaj_kitle = new List<toplu_mesaj_kitle>();
        }

        public int id { get; set; }
        public int okul_id { get; set; }
        public string baslik { get; set; }
        public string icerik { get; set; }
        public System.DateTime bas_tarih { get; set; }
        public System.DateTime son_tarih { get; set; }
        public System.DateTime olusturulma { get; set; }
        public byte silindi { get; set; }
        public byte seviye_mi { get; set; }
        public virtual ICollection<indirim_uye> indirim_uye { get; set; }
        public virtual ICollection<toplu_mesaj_kitle> toplu_mesaj_kitle { get; set; }
    }
}
