using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class seviye_odulleri
    {
        public int id { get; set; }
        public int seviye_id { get; set; }
        public string baslik { get; set; }
        public string baslik_ingilizce { get; set; }
        public string aciklama { get; set; }
        public string aciklama_ingilizce { get; set; }
        public virtual uye_seviyeleri uye_seviyeleri { get; set; }
    }
}
