using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class AnasayfaIcerikModel
    {
        public AnasayfaEnlerModel enler { get; set; }
        public List<AnasayfaTabModel> tabs { get; set; }
        public List<AnasayfaYorumModel> yorumlar { get; set; }
        public string arama_back { get; set; }
        public AnasayfaYorumModel ilkyorum { get; set; }
    }
}