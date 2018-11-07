using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class AnasayfaEnlerModel
    {
        public List<AnasayfaEnlerOkulModel> goruntulenenler { get; set; }
        public List<AnasayfaEnlerOkulModel> yorum_alanlar { get; set; }
        public List<AnasayfaEnlerOkulModel> EnPopulerSehirler { get; set; }
        public List<AnasayfaEnlerOkulModel> EnBegenilenUniversiteler { get; set; }
        public List<AnasayfaEnlerOkulModel> EnBegenilenDilOkullari { get; set; }
        public List<AnasayfaEnlerOkulModel> EnBegenilenKolejler { get; set; }
        public List<AnasayfaEnlerOkulModel> EnBegenilenLiseler { get; set; }
    }
}