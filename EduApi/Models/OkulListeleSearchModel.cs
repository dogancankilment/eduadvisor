﻿namespace EduApi.Models
{
    public class OkulListeleSearchModel
    {
        public string puan_turleri { get; set; }
        public string aranacak_kelime { get; set; }
        public string egitim_id { get; set; }
        public string ulke_id { get; set; }
        public string sehir_id { get; set; }
        public int sirala { get; set; }
        public int page { get; set; }
    }
}