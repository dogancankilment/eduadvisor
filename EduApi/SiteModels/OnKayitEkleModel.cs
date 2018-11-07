namespace EduApi.SiteModels
{
    public class OnKayitEkleModel
    {
        public int okul_id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int cinsiyet { get; set; }
        public int milliyet { get; set; }
        public string dogum_tarih { get; set; }
        public int dogum_ulke { get; set; }
        public int dogum_sehir { get; set; }
        public int yasadigi_ulke { get; set; }
        public int yasadigi_sehir { get; set; }
        public string pass_no { get; set; }
        public string pass_tarih { get; set; }
        public string acik_adres { get; set; }
        public string ev_no { get; set; }
        public string cep_no { get; set; }
        public string mail { get; set; }
        public string baslama_tarih { get; set; }
        public int program_tur_id { get; set; }
        public int program_id { get; set; }
        public int kurs_hafta { get; set; }
        public string dil_seviye { get; set; }
    }
}