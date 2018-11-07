using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class egitim_turleri
    {
        public egitim_turleri()
        {
            this.Anketlers = new List<Anketler>();
            this.egitim_ozellikleri = new List<egitim_ozellikleri>();
            this.fiyat_tur = new List<fiyat_tur>();
            this.kurumsal_kayit = new List<kurumsal_kayit>();
            this.okul_gruplari = new List<okul_gruplari>();
            this.okullars = new List<okullar>();
            this.onkayit_egitim_turleri = new List<onkayit_egitim_turleri>();
            this.program_havuzu = new List<program_havuzu>();
            this.uye_ilgi = new List<uye_ilgi>();
        }

        public int id { get; set; }
        public string adi { get; set; }
        public string adi_ingilizce { get; set; }
        public byte silindi { get; set; }
        public string seo_url { get; set; }
        public string pin_adi { get; set; }
        public string harita_key_def { get; set; }
        public string harita_key_ozel { get; set; }
        public bool program_var { get; set; }
        public bool dil_bilgisi_alinacak { get; set; }
        public bool kurs_hafta_alinacak { get; set; }
        public virtual ICollection<Anketler> Anketlers { get; set; }
        public virtual ICollection<egitim_ozellikleri> egitim_ozellikleri { get; set; }
        public virtual ICollection<fiyat_tur> fiyat_tur { get; set; }
        public virtual ICollection<kurumsal_kayit> kurumsal_kayit { get; set; }
        public virtual ICollection<okul_gruplari> okul_gruplari { get; set; }
        public virtual ICollection<okullar> okullars { get; set; }
        public virtual ICollection<onkayit_egitim_turleri> onkayit_egitim_turleri { get; set; }
        public virtual ICollection<program_havuzu> program_havuzu { get; set; }
        public virtual ICollection<uye_ilgi> uye_ilgi { get; set; }
    }
}
