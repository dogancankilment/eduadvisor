using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class OkulDetayTabContentModel
    {
        public List<ResimYaziModel> slayt_resimleri { get; set; }
        public MapKonumModel harita_bilgileri { get; set; }
        public List<string> okulOzellik { get; set; }
        public List<string> okulProgram { get; set; }
      
    }
}