using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class okullar
    {
        public okullar()
        {
            this.anket_cevaplayan = new List<anket_cevaplayan>();
            this.fiyats = new List<fiyat>();
            this.kurumsal_yonetici_sube = new List<kurumsal_yonetici_sube>();
            this.kurumsal_yoneticiler = new List<kurumsal_yoneticiler>();
            this.okul_fakulteleri = new List<okul_fakulteleri>();
            this.okul_fiyat_ogr_tur = new List<okul_fiyat_ogr_tur>();
            this.okul_fotograflar = new List<okul_fotograflar>();
            this.okul_grup_iliski = new List<okul_grup_iliski>();
            this.okul_gunluk = new List<okul_gunluk>();
            this.okul_indirimleri = new List<okul_indirimleri>();
            this.okul_ozellikleri = new List<okul_ozellikleri>();
            this.okul_programlari = new List<okul_programlari>();
            this.okul_sistem = new List<okul_sistem>();
            this.okul_ziyaret = new List<okul_ziyaret>();
            this.on_kayit_basvuru = new List<on_kayit_basvuru>();
            this.uye_okullari = new List<uye_okullari>();
            this.yorums = new List<yorum>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public int egitim_id { get; set; }
        public string kurulus_tarihi { get; set; }
        public string logo { get; set; }
        public int ulke_id { get; set; }
        public int sehir_id { get; set; }
        public Nullable<int> ilce_id { get; set; }
        public string adres { get; set; }
        public string mobil_tel { get; set; }
        public string okul_email { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string fax { get; set; }
        public string web_site { get; set; }
        public string skype_id { get; set; }
        public string fiyat_link { get; set; }
        public string aciklama { get; set; }
        public byte yayinda { get; set; }
        public int merkez_id { get; set; }
        public System.DateTime okul_kayit_tarihi { get; set; }
        public string seo_url { get; set; }
        public bool arsivde { get; set; }
        public virtual ICollection<anket_cevaplayan> anket_cevaplayan { get; set; }
        public virtual egitim_duzeyleri egitim_duzeyleri { get; set; }
        public virtual egitim_turleri egitim_turleri { get; set; }
        public virtual ICollection<fiyat> fiyats { get; set; }
        public virtual ICollection<kurumsal_yonetici_sube> kurumsal_yonetici_sube { get; set; }
        public virtual ICollection<kurumsal_yoneticiler> kurumsal_yoneticiler { get; set; }
        public virtual ICollection<okul_fakulteleri> okul_fakulteleri { get; set; }
        public virtual ICollection<okul_fiyat_ogr_tur> okul_fiyat_ogr_tur { get; set; }
        public virtual ICollection<okul_fotograflar> okul_fotograflar { get; set; }
        public virtual ICollection<okul_grup_iliski> okul_grup_iliski { get; set; }
        public virtual ICollection<okul_gunluk> okul_gunluk { get; set; }
        public virtual ICollection<okul_indirimleri> okul_indirimleri { get; set; }
        public virtual okul_lokasyon okul_lokasyon { get; set; }
        public virtual ICollection<okul_ozellikleri> okul_ozellikleri { get; set; }
        public virtual ICollection<okul_programlari> okul_programlari { get; set; }
        public virtual okul_sayac okul_sayac { get; set; }
        public virtual ICollection<okul_sistem> okul_sistem { get; set; }
        public virtual ICollection<okul_ziyaret> okul_ziyaret { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public virtual ICollection<uye_okullari> uye_okullari { get; set; }
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
