namespace EduApi.Models
{
    public class YeniOkulYorumModel
    {
        public string uye_id { get; set; }
        public string baslik { get; set; }
        public string program_id { get; set; }
        public string program_turu { get; set; }
        public string puani { get; set; }
        public string yorumu { get; set; }
        public string egitim_baslangic { get; set; }
        public string egitim_bitis { get; set; }
        public string resim1 { get; set; }
        public string resim2 { get; set; }
        public string resim3 { get; set; }
        public string okul_id { get; set; }
    }
}