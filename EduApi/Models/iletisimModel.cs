using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduApi.Models
{
    public class iletisimModel
    {
        public string eposta { get; set; }
        public string konu { get; set; }
        public string mesaj { get; set; }
        public string uye_id { get; set; }
        public string adsoyad { get; set; }
        public string telefon { get; set; }
    }
}