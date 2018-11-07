using dll.Models;
using System.Collections.Generic;

namespace EduApi.Models
{
    public class ApiOkulDetayModel
    {
        public string tam_adi { get; set; }
        public string aciklama { get; set; }
        public string logo { get; set; }
        public string okul_adi { get; set; }
        public string grup_adi { get; set; }
        public string web_site_link { get; set; }
        public string puani { get; set; }
        public List<TextValueModel> resimler { get; set; }
        public string indirim_orani { get; set; }
        public List<string> ozellikler { get; set; }
        public List<string> programlari { get; set; }
        public List<YorumModel> yorumlar { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }
}