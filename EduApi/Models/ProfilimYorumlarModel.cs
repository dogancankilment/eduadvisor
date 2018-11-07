using System.Collections.Generic;

namespace EduApi.Models
{
    public class ProfilimYorumlarModel
    {
        public string id { get; set; }
        public string baslik { get; set; }
        public string tarih { get; set; }
        public string okul_adi { get; set; }
        public string puani { get; set; }
        public string durumu { get; set; }
        public string icerik { get; set; }
        public List<TextValueModel> resimler { get; set; }
        public bool resim_1_degisti { get; set; }
        public bool resim_2_degisti { get; set; }
        public bool resim_3_degisti { get; set; }
    }
}