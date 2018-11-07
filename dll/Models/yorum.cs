using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class yorum
    {
        public yorum()
        {
            this.yorum_begeni_okul = new List<yorum_begeni_okul>();
            this.yorum_begeniler = new List<yorum_begeniler>();
            this.yorum_resimleri = new List<yorum_resimleri>();
            this.yorum_sikayetleri = new List<yorum_sikayetleri>();
        }

        public int id { get; set; }
        public string baslik { get; set; }
        public string icerik { get; set; }
        public Nullable<int> uye_id { get; set; }
        public System.DateTime tarih { get; set; }
        public Nullable<int> okul_id { get; set; }
        public int puani { get; set; }
        public string okul_bas { get; set; }
        public string okul_bit { get; set; }
        public int onay { get; set; }
        public Nullable<System.DateTime> onaylanma_tarihi { get; set; }
        public Nullable<System.DateTime> silinme_tarihi { get; set; }
        public Nullable<int> program_id { get; set; }
        public Nullable<int> program_tur_id { get; set; }
        public bool uye_sildi { get; set; }
        public Nullable<System.DateTime> uye_silme_tarihi { get; set; }
        public virtual okullar okullar { get; set; }
        public virtual onkayit_egitim_turleri onkayit_egitim_turleri { get; set; }
        public virtual program_havuzu program_havuzu { get; set; }
        public virtual uyeler uyeler { get; set; }
        public virtual ICollection<yorum_begeni_okul> yorum_begeni_okul { get; set; }
        public virtual ICollection<yorum_begeniler> yorum_begeniler { get; set; }
        public virtual ICollection<yorum_resimleri> yorum_resimleri { get; set; }
        public virtual ICollection<yorum_sikayetleri> yorum_sikayetleri { get; set; }
        public virtual yorum_yanitlari yorum_yanitlari { get; set; }
    }
}
