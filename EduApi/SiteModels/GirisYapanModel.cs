using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class GirisYapanModel
    {
        public int okunmayan_mesaj { get; set; }
        public string adi { get; set; }
        public string fotograf { get; set; }
        public int okunmayan_bildirim { get; set; }
        public List<string> gelen_mesajlar { get; set; }
        public List<ValueTextModel> kampanya_bildirimler { get; set; }
        public List<ValueTextModel> edu_bildirimler { get; set; }
        public bool dogrulandi { get; set; }
    }
}