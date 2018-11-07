using System;

namespace EduApi.SiteModels
{
    public class AnasayfaYorumModel
    {
        public string image_url { get; set; }
        public DateTime yorum_tarih { get; set; }
        public string adi { get; set; }
        public string okul_adi { get; set; }
        public string yorum_baslik { get; set; }
        public int yorum_puani { get; set; }
        public string yorum_icerik { get; set; }
        public string okul_seo { get; set; }
        public string grup_seo { get; set; }
        public string seviyesi { get; set; }
        public string okul_id { get; set; }
    }
}