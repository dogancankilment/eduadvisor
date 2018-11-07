using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class sorular
    {
        public int id { get; set; }
        public string soru { get; set; }
        public string soru_ingilizce { get; set; }
        public string cevap { get; set; }
        public string cevap_ingilizce { get; set; }
        public int sira_no { get; set; }
    }
}
