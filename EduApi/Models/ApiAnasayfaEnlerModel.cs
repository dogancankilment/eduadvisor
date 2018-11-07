using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApi.Models
{
    public class ApiAnasayfaEnlerModel
    {
        public List<ApiAnasayfaEnlerOkulModel> goruntulenenler { get; set; }
        public List<ApiAnasayfaEnlerOkulModel> yorum_alanlar { get; set; }
        public List<ApiAnasayfaEnlerOkulModel> EnPopulerSehirler { get; set; }
        public List<ApiAnasayfaEnlerOkulModel> EnBegenilenUniversiteler { get; set; }
        public List<ApiAnasayfaEnlerOkulModel> EnBegenilenDilOkullari { get; set; }
        public List<ApiAnasayfaEnlerOkulModel> EnBegenilenKolejler { get; set; }
        public List<ApiAnasayfaEnlerOkulModel> EnBegenilenLiseler { get; set; }
    }
    public class ApiAnasayfaEnlerOkulModel
    {
        public string grup_seo { get; set; }
        public string okul_adi { get; set; }
        public string deger { get; set; }
        public string yer { get; set; }
    }
}