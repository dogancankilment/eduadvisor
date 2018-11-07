using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class AnasayfaYorumOkulModel
    {
        public List<AnasayfaYorumModel> yorumlar { get; set; }
        public List<AnasayfaOkullarModel> okullar { get; set; }
    }
}