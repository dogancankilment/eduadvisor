using EduApi.SiteModels;
using System.Collections.Generic;

namespace EduApi.Models
{
    public class GrupDetayModel
    {
        public List<ApiGrupDetayOkulModel> okullar { get; set; }
        public string logo { get; set; }
        public string adi { get; set; }
        public string puani { get; set; }
        public string seo_url { get; set; }
        public string egitim_id { get; set; }
    }
}