using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class on_kayit_basvuru
    {
        public int id { get; set; }
        public int uye_id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int cinsiyet { get; set; }
        public int uyruk { get; set; }
        public string dog_tar { get; set; }
        public string email { get; set; }
        public int dogum_ulke { get; set; }
        public int dogum_il { get; set; }
        public string pass_no { get; set; }
        public string pass_tarih { get; set; }
        public Nullable<int> yas_ulke { get; set; }
        public Nullable<int> yas_il { get; set; }
        public string adres { get; set; }
        public string tel_ev { get; set; }
        public string tel_cep { get; set; }
        public Nullable<int> program_tur_id { get; set; }
        public Nullable<int> program_id { get; set; }
        public string mezun_okul { get; set; }
        public string on_kayitkodu { get; set; }
        public string kurs_hafta { get; set; }
        public string dil_seviye { get; set; }
        public System.DateTime basvuru_tarihi { get; set; }
        public int basvurdugu_okul { get; set; }
        public string baslayacagi_tarih { get; set; }
        public int durumu { get; set; }
        public byte okundu { get; set; }
        public virtual il il { get; set; }
        public virtual il il1 { get; set; }
        public virtual okullar okullar { get; set; }
        public virtual ulke ulke { get; set; }
        public virtual onkayit_egitim_turleri onkayit_egitim_turleri { get; set; }
        public virtual program_havuzu program_havuzu { get; set; }
        public virtual uyeler uyeler { get; set; }
        public virtual ulke ulke1 { get; set; }
        public virtual ulke ulke2 { get; set; }
    }
}
