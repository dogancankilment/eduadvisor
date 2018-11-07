namespace EduApi.SiteModels
{
    public class FacebookProfilModel
    {
        public string Id { get; set; }
        public string kullanici_adi { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string ProfilResmi { get; set; }
        public string Email { get; set; }
        public string link { get; set; }
        public string yas { get; set; }
        public string locale { get; set; }
        public string Cinsiyet { get; set; }
        public FaceBookEntity Yer { get; set; }
    }
    public class FaceBookEntity
    {
        public string Id { get; set; }
        public string Adi { get; set; }
    }

}