namespace EduApi.Models
{
    public class YeniOkulModel
    {
        public string ekleyen_tip { get; set; }
        public string egitim_id { get; set; }
        public string fakulte_adi { get; set; }
        public string grup_adi { get; set; }
        public string ulke_adi { get; set; }
        public string sehir_adi { get; set; }
        public string okul_adi { get; set; }
        public YeniOkulYorumModel ogrenci { get; set; }
        public YeniOkulYetkiliModel yetkili { get; set; }
    }
    public class YeniOkulYetkiliModel
    {
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string telefon { get; set; }
        public string mail { get; set; }
        public string skype_id { get; set; }
        public string mesajiniz { get; set; }
    }
}