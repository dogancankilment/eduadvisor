using EduApi.SiteModels;
using Facebook;
using System;

namespace SiteMvc.App_Classes
{
    public class FacebookIslemleri
    {
        string AppID = "166082627507948";
        string AppSecret = "533ee430421aff5673c03805fc34a3c4";

        string CallBackUrl = "https://eduadvisor.co.uk/Facebook-Profil";

        /*

         Facebook Login için kullanıcıyı Facebook'a yönlendirirsiniz.Daha sonra
         kullanıcı gerekli izinleri verdikten sonra Facebook o kullanıcıyı size
         yeniden yönlendirecektir.CallBackUrl değeri ile Facebook'a, kullanıcı
         izin verdikten ve senle işi bittikten sonra şu adresime yönlendir.

         */

        private readonly string Scope = "public_profile, email,user_birthday,user_location";
        /*

         Kullanıcının hangi bilgilerine ihtiyaç duyduğunuzu Scope ile belirtirsiniz.
         Başka bir deyişle kullanıcının hangi bilgilerine ulaşmak istiyorsanız
         o bilgileri size ulaştıracak anahtar sözcükleri Scope'a yazarsınız.

         Tabi kullanıcı onayı olmadan buradaki bilgilerin hiçbirine ulaşım söz
         konusu değildir.

        */

        FacebookClient FacebookIslem = new FacebookClient();
        //FacebookClient SDK'nın sağladığı sınıftır.

        public Uri CrateLoginUrl()
        {
            /*

             Bu metot da kullanıcıdan Facebook login talebi geldinğinde
             kullanıcıyı yönlendireceğimiz Facebook linkini oluşturuyoruz.

             */

            return FacebookIslem.GetLoginUrl(
                                new
                                {
                                    client_id = AppID,
                                    client_secret = AppSecret,
                                    redirect_uri = CallBackUrl,
                                    response_type = "code",
                                    scope = Scope,
                                });
        }

        public dynamic GetAccessToken(string code, string state, string type)
        {
            /*

             AccessToken Facebook'un size verdiği erişim kodudur.OAuth
             konusuna bakarsak ne olduğunu daha iyi anlayabiliriz.
             En kısa tanımla OAuth, kullanıcıların üyesi oldukları
             bir site yada platformun şifresini üye oldukları başka
             bir web sitesi yada platformla paylaşmadan, izin verdiği
             bilgilere diğer site tarafından ulaşılmasını sağlayan
             bir kimlik doğrulama protokolüdür.

             Yani, AccessToken kodu ile istediğimiz kullanıcı bilgilerini
             çekebiliyoruz.Ne de olsa bu kod, kullanıcının istediğimiz
             bilgilerini elde etmemize izin verdiğini gösteriyor.

             */

            dynamic result = FacebookIslem.Post("oauth/access_token",
                                          new
                                          {
                                              client_id = AppID,
                                              client_secret = AppSecret,
                                              redirect_uri = CallBackUrl,
                                              code = code
                                          });
            return result.access_token;
        }

        public FacebookProfilModel GetUserInfo(dynamic accessToken)
        {
            //Kullanıcı bilgilerini çektiğimiz metod.

            var client = new FacebookClient(accessToken);
            var profile = new FacebookProfilModel();
            dynamic me = client.Get("/me?fields=id,cover,name,first_name,last_name,age_range,link,gender,locale,email,location");
            /*
             ,cover,name,first_name,last_name,age_range,link,gender,locale,email,location
             "/me" yerine kullanıcının Facebook ID'sini de yazabilirsiniz."me"
             o anki kullanıcıyı temsil etmektedir.Yani değişen birşey yok.

             */

            profile.kullanici_adi = me.name;
            profile.isim = me.first_name;
            profile.soyisim = me.last_name;
            profile.Id = me.id;
            profile.Email = me.email;
            profile.yas = me.age_range.min.ToString();
            profile.link = me.link;
            if (me.location != null)
            {
                FaceBookEntity fe = new FaceBookEntity();
                fe.Id = me.location.id;
                fe.Adi = me.location.name;
                profile.Yer = fe;
            }
            profile.locale = me.locale;
            profile.Cinsiyet = me.gender;
            profile.ProfilResmi = string.Format("http://graph.facebook.com/{0}/picture", profile.Id);
            return profile;
        }
    }
}