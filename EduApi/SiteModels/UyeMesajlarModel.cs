using dll.Models;
using System.Collections.Generic;

namespace EduApi.SiteModels
{
    public class UyeMesajlarModel
    {
        public List<uye_mesajlari> mesajlar { get; set; }
        public List<UyeKampanyalarModel> kampanyalar { get; set; }
        public List<toplu_mesaj> bildirimler { get; set; }
    }
}