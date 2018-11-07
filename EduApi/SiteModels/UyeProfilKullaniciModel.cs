using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class UyeProfilKullaniciModel
    {
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string cinsiyet { get; set; }
        public string mail { get; set; }
        public string yas { get; set; }
        public string adres { get; set; }
        public string tel { get; set; }
        public string biyografi { get; set; }
        public string haber { get; set; }
        public List<int> ilgiler { get; set; }
        public string ulke_kodu { get; set; }
        public string bolge_kodu { get; set; }
    }
}