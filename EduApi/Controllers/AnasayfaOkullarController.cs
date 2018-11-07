using EduApi.Siniflar;
using EduApi.SiteModels;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class AnasayfaOkullarController : ApiController
    {
        public AnasayfaEnlerModel Get()
        {
            return site_islem.AnasayfaOkullar("tr-TR");
            #region sifreleme
            //List<ApiAnasayfaEnlerOkulModel> EnBegenilenDilOkullari = new List<ApiAnasayfaEnlerOkulModel>();
            //List<ApiAnasayfaEnlerOkulModel> EnBegenilenKolejler = new List<ApiAnasayfaEnlerOkulModel>();
            //List<ApiAnasayfaEnlerOkulModel> EnBegenilenLiseler = new List<ApiAnasayfaEnlerOkulModel>();
            //List<ApiAnasayfaEnlerOkulModel> EnBegenilenUniversiteler = new List<ApiAnasayfaEnlerOkulModel>();
            //List<ApiAnasayfaEnlerOkulModel> goruntulenenler = new List<ApiAnasayfaEnlerOkulModel>();
            //List<ApiAnasayfaEnlerOkulModel> EnPopulerSehirler = new List<ApiAnasayfaEnlerOkulModel>();
            //List<ApiAnasayfaEnlerOkulModel> yorum_alanlar = new List<ApiAnasayfaEnlerOkulModel>();
            //islem.degerleriKaristir();
            //#region EnBegenilenDilOkullari
            //for (int i = 0; i < gelen.EnBegenilenDilOkullari.Count; i++)
            //{
            //    EnBegenilenDilOkullari.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.EnBegenilenDilOkullari[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.EnBegenilenDilOkullari[i].okul_adi),
            //        grup_seo = islem.Crypto(gelen.EnBegenilenDilOkullari[i].grup_seo)
            //    });
            //}
            //#endregion
            //#region EnBegenilenKolejler
            //for (int i = 0; i < gelen.EnBegenilenKolejler.Count; i++)
            //{
            //    EnBegenilenKolejler.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.EnBegenilenKolejler[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.EnBegenilenKolejler[i].okul_adi),
            //        grup_seo = islem.Crypto(gelen.EnBegenilenKolejler[i].grup_seo)
            //    });
            //}
            //#endregion
            //#region EnBegenilenLiseler
            //for (int i = 0; i < gelen.EnBegenilenLiseler.Count; i++)
            //{
            //    EnBegenilenLiseler.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.EnBegenilenLiseler[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.EnBegenilenLiseler[i].okul_adi),
            //        grup_seo = islem.Crypto(gelen.EnBegenilenLiseler[i].grup_seo)
            //    });
            //}
            //#endregion
            //#region EnBegenilenUniversiteler
            //for (int i = 0; i < gelen.EnBegenilenUniversiteler.Count; i++)
            //{
            //    EnBegenilenUniversiteler.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.EnBegenilenUniversiteler[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.EnBegenilenUniversiteler[i].okul_adi),
            //        grup_seo = islem.Crypto(gelen.EnBegenilenUniversiteler[i].grup_seo)
            //    });
            //}
            //#endregion
            //#region goruntulenenler
            //for (int i = 0; i < gelen.goruntulenenler.Count; i++)
            //{
            //    goruntulenenler.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.goruntulenenler[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.goruntulenenler[i].okul_adi),
            //        grup_seo = islem.Crypto(gelen.goruntulenenler[i].grup_seo)
            //    });
            //}
            //#endregion
            //#region EnPopulerSehirler
            //for (int i = 0; i < gelen.EnPopulerSehirler.Count; i++)
            //{
            //    EnPopulerSehirler.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.EnPopulerSehirler[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.EnPopulerSehirler[i].okul_adi),
            //    });
            //}
            //#endregion
            //#region yorum_alanlar
            //for (int i = 0; i < gelen.yorum_alanlar.Count; i++)
            //{
            //    yorum_alanlar.Add(new ApiAnasayfaEnlerOkulModel()
            //    {
            //        deger = islem.Crypto(gelen.yorum_alanlar[i].deger.ToString()),
            //        okul_adi = islem.Crypto(gelen.yorum_alanlar[i].okul_adi),
            //        grup_seo = islem.Crypto(gelen.yorum_alanlar[i].grup_seo)
            //    });
            //}
            //#endregion
            //if (EnBegenilenDilOkullari.Count > 0)
            //    EnBegenilenDilOkullari[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //else if (EnBegenilenKolejler.Count > 0)
            //    EnBegenilenKolejler[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //else if (EnBegenilenLiseler.Count > 0)
            //    EnBegenilenLiseler[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //else if (EnBegenilenUniversiteler.Count > 0)
            //    EnBegenilenUniversiteler[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //else if (goruntulenenler.Count > 0)
            //    goruntulenenler[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //else if (EnPopulerSehirler.Count > 0)
            //    EnPopulerSehirler[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //else if (yorum_alanlar.Count > 0)
            //    yorum_alanlar[0].okul_adi += "Gk02+Lm/" + islem.topla();
            //ApiAnasayfaEnlerModel donecek = new ApiAnasayfaEnlerModel()
            //{
            //    EnBegenilenDilOkullari = EnBegenilenDilOkullari,
            //    EnBegenilenKolejler = EnBegenilenKolejler,
            //    EnBegenilenLiseler = EnBegenilenLiseler,
            //    EnBegenilenUniversiteler = EnBegenilenUniversiteler,
            //    EnPopulerSehirler = EnPopulerSehirler,
            //    goruntulenenler = goruntulenenler,
            //    yorum_alanlar = yorum_alanlar
            //};
            #endregion
        }
    }
}