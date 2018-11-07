using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class OkulDetayYorumModel
    {
        public string baslik { get; set; }
        public string uye_adi_tarih { get; set; }
        public string puani { get; set; }
        public string begeni_sayisi { get; set; }
        public string programi { get; set; }
        public string yorumu { get; set; }
        public string uye_resim { get; set; }
        public string yanitlandi { get; set; }
        public string yanit_resim_url { get; set; }
        public string yanit_icerik { get; set; }
        public List<string> begenenler { get; set; }
        public List<string> resimler { get; set; }
    }
}