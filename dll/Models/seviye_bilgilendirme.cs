using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class seviye_bilgilendirme
    {
        public int id { get; set; }
        public string baslik { get; set; }
        public string baslik_ingilizce { get; set; }
        public string icerik { get; set; }
        public string icerik_ingilizce { get; set; }
        public int sira_no { get; set; }
    }
}
