using System.Collections.Generic;

namespace EduApi.Models
{
    public class ProfilimKullaniciBilgileriModel
    {
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string cinsiyet { get; set; }
        public string email { get; set; }
        public string yas { get; set; }
        public string ulke_id { get; set; }
        public string sehir_id { get; set; }
        public string telefon { get; set; }
        public string biyografi { get; set; }
        public string haber_bulten { get; set; }
        public List<string> ilgili_egitimler { get; set; }
    }
}