using EduApi.Models;
using EduApi.Siniflar;
using System.Collections.Generic;
using System.Web.Http;

namespace EduApi.Controllers
{
    public class GrupDetayController : ApiController
    {
        public List<ApiGrupDetayOkulModel> Post(GrupDetayPostModel gelen)
        {
            // islem.keyayikla(gelen.seo_url);
            // gelen.seo_url = islem.Decrypt(gelen.seo_url.Substring(0, gelen.seo_url.IndexOf("Gk02Lm")));
            List<ApiGrupDetayOkulModel> donecek = islem.GrupDetayGetir(gelen.seo_url);
            //islem.degerleriKaristir();
            //for (int i = 0; i < donecek.okullar.Count; i++)
            //{
            //    donecek.okullar[i].okul_adi = islem.Crypto(donecek.okullar[i].okul_adi);
            //    donecek.okullar[i].ulke_adi = islem.Crypto(donecek.okullar[i].ulke_adi);
            //    donecek.okullar[i].sehir_adi = islem.Crypto(donecek.okullar[i].sehir_adi);
            //    donecek.okullar[i].seo_url = islem.Crypto(donecek.okullar[i].seo_url);
            //    donecek.okullar[i].fakulte_adi = islem.Crypto(donecek.okullar[i].fakulte_adi);
            //    donecek.okullar[i].id = islem.Crypto(donecek.okullar[i].id);
            //}
            //donecek.adi = islem.Crypto(donecek.adi);
            //donecek.logo = islem.Crypto(donecek.logo);
            //donecek.puani = islem.Crypto(donecek.puani);
            //donecek.okullar[0].okul_adi += "Gk02+Lm/" + islem.topla();
            return donecek;
        }
    }
}