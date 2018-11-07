using dll.Models;
using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class UyeSeviyeModel
    {
        public List<uye_seviyeleri> seviyeler { get; set; }
        public List<seviye_bilgilendirme> bilgilendirmeler { get; set; }
 
    }
}