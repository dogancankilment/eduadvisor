namespace EduApi.Models
{
    public class HaritaOkulModel
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
        public int id { get; set; }
        public string seo_url { get; set; }
    }
}