using System.Collections.Generic;

namespace EduApi.Models
{
    public class YorumModel
    {
        public string baslik { get; set; }
        public string uye_adi_tarih { get; set; }
        public string puani_begeni_sayisi { get; set; }
        public string programi { get; set; }
        public string yorumu { get; set; }
        public string uye_resim { get; set; }
        public List<string> resimler { get; set; }
        public string yanitlandi { get; set; }
        public string yanit_resim_url { get; set; }
        public string yanit_icerik { get; set; }
    }
}