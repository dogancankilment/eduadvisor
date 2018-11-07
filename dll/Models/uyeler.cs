using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class uyeler
    {
        public uyeler()
        {
            this.anket_cevaplayan = new List<anket_cevaplayan>();
            this.indirim_uye = new List<indirim_uye>();
            this.okul_ziyaret = new List<okul_ziyaret>();
            this.on_kayit_basvuru = new List<on_kayit_basvuru>();
            this.uye_ilgi = new List<uye_ilgi>();
            this.uye_okullari = new List<uye_okullari>();
            this.yorum_begeniler = new List<yorum_begeniler>();
            this.yorum_sikayetleri = new List<yorum_sikayetleri>();
            this.yorums = new List<yorum>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string fotograf { get; set; }
        public string mail { get; set; }
        public Nullable<int> Cinsiyet { get; set; }
        public int il_id { get; set; }
        public int ilce_id { get; set; }
        public int ulke_id { get; set; }
        public string yas { get; set; }
        public System.DateTime tarih { get; set; }
        public Nullable<byte> haber_bulten { get; set; }
        public string sifre { get; set; }
        public Nullable<int> kayit_tur { get; set; }
        public string biyografi { get; set; }
        public string tel_no { get; set; }
        public int puani { get; set; }
        public string hesap_dondur { get; set; }
        public int hesap_aktiflik { get; set; }
        public Nullable<System.DateTime> dondurma_tarihi { get; set; }
        public byte kara_liste { get; set; }
        public Nullable<System.DateTime> kara_liste_tarih { get; set; }
        public byte silindi { get; set; }
        public Nullable<System.DateTime> silinme_tarihi { get; set; }
        public string uye_adres { get; set; }
        public string tel_bolge_kodu { get; set; }
        public string tel_ulke_kodu { get; set; }
        public virtual ICollection<anket_cevaplayan> anket_cevaplayan { get; set; }
        public virtual ICollection<indirim_uye> indirim_uye { get; set; }
        public virtual ICollection<okul_ziyaret> okul_ziyaret { get; set; }
        public virtual ICollection<on_kayit_basvuru> on_kayit_basvuru { get; set; }
        public virtual uye_dogrulama uye_dogrulama { get; set; }
        public virtual ICollection<uye_ilgi> uye_ilgi { get; set; }
        public virtual ICollection<uye_okullari> uye_okullari { get; set; }
        public virtual ICollection<yorum_begeniler> yorum_begeniler { get; set; }
        public virtual ICollection<yorum_sikayetleri> yorum_sikayetleri { get; set; }
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
