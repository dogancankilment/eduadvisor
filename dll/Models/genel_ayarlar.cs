using System;
using System.Collections.Generic;

namespace dll.Models
{
    public partial class genel_ayarlar
    {
        public string firma_adi { get; set; }
        public string firma_logo { get; set; }
        public Nullable<byte> recaptcha { get; set; }
        public string smtp_user { get; set; }
        public string smtp_pass { get; set; }
        public Nullable<int> smtp_port { get; set; }
        public string smtp_host { get; set; }
        public string smtp_mail { get; set; }
        public Nullable<byte> bakim_modu { get; set; }
        public string seo_anahtar { get; set; }
        public string seo_aciklama { get; set; }
        public string copyright_satiri_turkce { get; set; }
        public string copyright_satiri_ingilizce { get; set; }
        public string copyright_satiri_gavurca { get; set; }
        public Nullable<byte> ziyaretci_sayaci_aktif { get; set; }
        public Nullable<int> ilk_yorum_tur { get; set; }
        public Nullable<int> ilk_yorum_id { get; set; }
        public string arama_back { get; set; }
    }
}
