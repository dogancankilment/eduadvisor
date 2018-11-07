using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class SiteGrupDetayModel
    {
        public List<GrupDetayOkulModel> okullar { get; set; }
        public string logo { get; set; }
        public string adi { get; set; }
        public string puani { get; set; }
        public string seo_url { get; set; }
        public int egitim_id { get; set; }
    }
}