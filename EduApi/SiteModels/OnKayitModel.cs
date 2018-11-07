using dll.Models;
using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class OnKayitModel
    {
        public int okul_id { get; set; }
        public egitim_turleri egitim_turu { get; set; }
        public string logo { get; set; }
        public string okul_adi { get; set; }
        public string Cinsiyet { get; set; }
        public string Uye_Adi { get; set; }
        public string Uye_Soyadi { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int ulke_id { get; set; }
        public int il_id { get; set; }
        public List<ValueTextModel> programlar { get; set; }
        public List<ValueTextModel> program_turleri { get; set; }
        public string uye_adres { get; set; }
    }
}