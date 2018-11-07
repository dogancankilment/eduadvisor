using System.Web.Mvc;
using System.Web.Routing;

namespace SiteMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("SeoYap", "SeoCreate", new { controller = "Seo", action = "Index" });
            routes.MapRoute("SifremiUnuttum", "Sifremi-Unuttum", new { controller = "Home", action = "SifremiUnuttum" }); 
            routes.MapRoute("GirisYap", "GirisYap", new { controller = "Uye", action = "GirisYap" });
            routes.MapRoute("CikisYap", "Cikis", new { controller = "Uye", action = "CikisYap" });
            routes.MapRoute("FacebookGirisYap", "FacebookGirisYap", new { controller = "Home", action = "FacebookGirisYap" }); 
            routes.MapRoute("FacebookProfil", "Facebook-Profil", new { controller = "Home", action = "FacebookProfil" });
            routes.MapRoute("GoogleGirisYap", "GoogleGirisYap", new { controller = "Home", action = "GoogleGirisYap" });
            routes.MapRoute("GoogleProfil", "Google-Profil", new { controller = "Home", action = "GoogleProfil" });
            routes.MapRoute("LinkedinGirisYap", "LinkedinGirisYap", new { controller = "Home", action = "LinkedinGirisYap" });
            routes.MapRoute("LinkedinProfil", "Linkedin-Profil", new { controller = "Home", action = "LinkedinProfil" });
            routes.MapRoute("BireyselKayitOl", "BireyselKayitOl", new { controller = "Uye", action = "KayitOl" }); 
            routes.MapRoute("DilDegistir", "DilDegistir", new { controller = "Home", action = "DilDegistir" });
            routes.MapRoute("YorumYap", "YorumYap", new { controller = "OkulSonuclari", action = "YorumYap" }); 
            routes.MapRoute("UyeMesajlar", "Uye-Mesajlar", new { controller = "Uye", action = "Mesajlarim" });
            routes.MapRoute("YakinOkullar", "Yakin-Okullar", new { controller = "Home", action = "YakinOkullar" });
            routes.MapRoute("UlkeSehirEyaletGetir", "UlkeSehirEyaletGetir", new { controller = "Home", action = "UlkeSehirEyaletGetir" });
            routes.MapRoute("OkulOnerisiGetir", "OkulOnerisiGetir", new { controller = "Home", action = "OkulOnerisi" }); 

            routes.MapRoute("UyeDogrula", "Uye-Dogrula/{dog_kod}", new { controller = "Uye", action = "UyeDogrula" });
            routes.MapRoute("UyeProfil", "Uye-Profil", new { controller = "Uye", action = "UyeProfil" });
            routes.MapRoute("UyeProfilKullanici", "UyeProfilKullanici", new { controller = "Uye", action = "UyeProfilKullanici" });
            routes.MapRoute("UyeProfilResmi", "UyeProfilResmi", new { controller = "Uye", action = "UyeProfilResmi" });
            routes.MapRoute("UyeProfilEgitimKampusler", "UyeProfilEgitimKampusler", new { controller = "Uye", action = "Kampusler" });
            routes.MapRoute("ProfilProgramTurleriGetir", "ProfilProgramTurleriGetir", new { controller = "Uye", action = "ProgramTurleri" });
            routes.MapRoute("ProfilProgramlariGetir", "ProfilProgramlariGetir", new { controller = "Uye", action = "Programlar" });
            routes.MapRoute("ProfilEgitimEkle", "ProfilEgitimEkle", new { controller = "Uye", action = "ProfilEgitimEkle" });
            routes.MapRoute("ProfilEgitimlerGetir", "ProfilEgitimlerGetir", new { controller = "Uye", action = "ProfilEgitimlerGetir" });
            routes.MapRoute("ProfilEgitimSil", "ProfilEgitimSil", new { controller = "Uye", action = "ProfilEgitimSil" });
            routes.MapRoute("ProfilSifreGuncelle", "ProfilSifreGuncelle", new { controller = "Uye", action = "SifreGuncelle" });
            routes.MapRoute("ProfilHesapDondur", "ProfilHesapDondur", new { controller = "Uye", action = "ProfilHesapDondur" });
            routes.MapRoute("ProfilYorumSil", "ProfilYorumSil", new { controller = "Uye", action = "ProfilYorumSil" });
            routes.MapRoute("ProfilYorumlariGetir", "ProfilYorumlariGetir", new { controller = "Uye", action = "ProfilYorumlariGetir" });
            routes.MapRoute("ProfilYorumDetayGetir", "ProfilYorumDetayGetir", new { controller = "Uye", action = "ProfilYorumDetayGetir" });
            routes.MapRoute("ProfilYorumResimSil", "ProfilYorumResimSil", new { controller = "Uye", action = "ProfilYorumResimSil" });
            routes.MapRoute("ProfilYorumGuncelle", "ProfilYorumGuncelle", new { controller = "Uye", action = "ProfilYorumGuncelle" });
            routes.MapRoute("ProfilUyeDogrula", "ProfilUyeDogrula", new { controller = "Uye", action = "ProfilUyeDogrula" });
            routes.MapRoute("ProfilTekrarKodGonder", "ProfilTekrarKodGonder", new { controller = "Uye", action = "ProfilTekrarKodGonder" });
            routes.MapRoute("MesajlariGetir", "UyeProfilMesajlariGetir", new { controller = "Uye", action = "MesajlariGetir" });
            routes.MapRoute("MesajGonder", "UyeProfilMesajGonder", new { controller = "Uye", action = "MesajGonder" });
            routes.MapRoute("MesajlariOkundu", "UyeProfilMesajlariOkundu", new { controller = "Uye", action = "MesajlariOkundu" });
            routes.MapRoute("UyeProfilBildirimSil", "UyeProfilBildirimSil", new { controller = "Uye", action = "BildirimSil" });
            routes.MapRoute("UyeProfilBildirimOnayla", "UyeProfilBildirimOnayla", new { controller = "Uye", action = "BildirimOnayla" });
            routes.MapRoute("UyeProfilOkullarGetir", "UyeProfilOkullarGetir", new { controller = "Uye", action = "UyeProfilOkullarGetir" });
            
            routes.MapRoute("UyeYorumBegen", "UyeYorumBegen", new { controller = "Uye", action = "YorumBegen" });
            routes.MapRoute("UyeYorumSikayetEt", "UyeYorumSikayetEt", new { controller = "Uye", action = "YorumSikayetEt" });

            routes.MapRoute("KampanyaBilgi", "Kampanya-Bilgilendirme", new { controller = "OkulSonuclari", action = "KampanyaDetayliBilgi" });

            routes.MapRoute("AnketGetir", "AnketGetir", new { controller = "OkulSonuclari", action = "AnketGetir" });
            routes.MapRoute("AnketGonder", "AnketGonder", new { controller = "OkulSonuclari", action = "AnketGonder" }); 
            routes.MapRoute("OkulSonuclari", "Okul/{kat}", new { controller = "OkulSonuclari", action = "Index" });
            routes.MapRoute("GenelBilgi", "Genel-Detay/{GrupSeo}", new { controller = "OkulSonuclari", action = "GenelBilgi" });
            routes.MapRoute("OkulDetay", "Okul-Detay/{GrupSeo}/{OkulSeo}-{id}", new { controller = "OkulSonuclari", action = "OkulDetay" });
            routes.MapRoute("UlkeGetir", "UlkeGetir", new { controller = "Home", action = "UlkeGetir" });
            routes.MapRoute("SehirleriGetir", "SehirleriGetir", new { controller = "Home", action = "SehirleriGetir" });
            routes.MapRoute("EyaletGetir", "EyaletGetir", new { controller = "Home", action = "EyaletGetir" });
            routes.MapRoute("EyaletSehirleriGetir", "EyaletSehirleriGetir", new { controller = "Home", action = "EyaletSehirleriGetir" });
            routes.MapRoute("SehirleriGetirTextValue", "SehirleriGetirTextValue", new { controller = "Home", action = "SehirleriGetirTextValue" });

            routes.MapRoute("KurumsalKayitOkulEkle", "KurumsalKayitOkulEkle", new { controller = "KurumsalKayit", action = "Ekle" });
            routes.MapRoute("KurumsalKayitOkullar", "KurumsalKayitOkullar", new { controller = "KurumsalKayit", action = "KurumsalKayitOkullar" });
            routes.MapRoute("ProgramlariGetir", "ProgramlariGetir", new { controller = "KurumsalKayit", action = "ProgramlariGetir" });            
            routes.MapRoute("KurumsalKayit", "Kurumsal-Kayit/{tip}", new { controller = "KurumsalKayit", action = "Index" });
            routes.MapRoute("KurumsalKayitFakulteler", "KurumsalKayitFakulteler", new { controller = "KurumsalKayit", action = "Fakulteler" }); 

            routes.MapRoute("SoruSor", "SoruSor", new { controller = "Home", action = "SoruSor" }); 
            routes.MapRoute("BizeUlasinMesajGonder", "BizeUlasinMesajGonder", new { controller = "Home", action = "BizeUlasin" });
            routes.MapRoute("AboneOl", "AboneOl", new { controller = "Home", action = "AboneOl" });
            
            routes.MapRoute("OnKayitForm", "On-Kayit/{GrupSeo}/{OkulSeo}", new { controller = "OkulSonuclari", action = "OnKayitForm" });


            routes.MapRoute("Footer", "{gozukecek}", new { controller = "Home", action = "Footer" });
        }
    }
}
