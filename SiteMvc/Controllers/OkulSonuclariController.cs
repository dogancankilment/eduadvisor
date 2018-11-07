using dll.Models;
using EduApi.Siniflar;
using EduApi.SiteModels;
using PagedList;
using SiteMvc.App_Classes;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SiteMvc.Controllers
{
    [AllActionRun]
    public class OkulSonuclariController : Controller
    {
        // GET: OkulSonuclari
        public ActionResult Index(string kat, string country = "-1", string city = "-1", string pt = "", string search = "", int? page = 1)
        {
            OkulSonuclariFiltreleModel kriterler = new OkulSonuclariFiltreleModel();
            kriterler.egitim_id = ViewBag.egitim_id = site_islem.SeoUrlEgitimIdGetir(kat);
            kriterler.ulke_id = ViewBag.ulke_id = country;
            kriterler.sehir_id = ViewBag.sehir_id = city;
            kriterler.puan_turleri = pt;
            kriterler.aranacak_kelime = ViewBag.aranacak_kelime = search;
            kriterler.sirala = 0;
            List<OkulSonuclariItemModel> sonuclar = site_islem.OkulListeleSonuclari(kriterler, HttpContext.Session["Dil"].ToString());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("Filtrele", sonuclar.ToPagedList(pageNumber, pageSize)) : View(sonuclar.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult GenelBilgi(string GrupSeo)
        {
            SiteGrupDetayModel detay = site_islem.GrupDetayGetir(GrupSeo, HttpContext.Session["Dil"].ToString());
            if (detay.okullar != null && detay.okullar.Count > 0)
                return View(detay);
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult OkulDetay(string GrupSeo, string OkulSeo, int id)
        {
            string mail = "-1";
            if (HttpContext.Session["giris_yapan"] != null)
                mail = HttpContext.Session["giris_yapan"].ToString();
            OkulDetayModel okul = site_islem.OkulDetayGetir(GrupSeo, OkulSeo, mail, HttpContext.Request.UserHostAddress, id);
            if (okul != null && okul.okul.id > 0)
            {
                ViewBag.GrupSeo = GrupSeo;
                return View(okul);
            }
            else return RedirectToAction("GenelBilgi", new { GrupSeo = GrupSeo });
        }
        public ActionResult KampanyaDetayliBilgi()
        {
            return View();
        }
        [Authorize]
        public ActionResult OnKayitForm(string OkulSeo, string GrupSeo)
        {
            if (HttpContext.Session["giris_yapan"] == null)
                return RedirectToAction("Index", "Home", new { ReturnUrl = "On-Kayit/" + GrupSeo + "/" + OkulSeo });
            OnKayitModel model = site_islem.OnKayitBilgileriGetir(GrupSeo, OkulSeo, HttpContext.Session["giris_yapan"].ToString());
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public string OnKayitForm(OnKayitEkleModel data)
        {
            if (HttpContext.Session["giris_yapan"] != null)
                return site_islem.OnKayitEkle(data, HttpContext.Session["giris_yapan"].ToString());
            else
                return "0";
        }
        [HttpPost]
        public string YorumYap(YorumModel data)
        {
            if (HttpContext.Session["giris_yapan"] != null)
                return site_islem.YorumEkle(data, HttpContext.Session["giris_yapan"].ToString());
            else
                return "-1";
        }
        [HttpPost]
        public PartialViewResult AnketGetir(int y_id)
        {
            Anketler anket = site_islem.AnketGetir(y_id);
            return PartialView(anket);
        }
        [HttpPost]
        public void AnketGonder(List<ValueTextModel> degerler, int y_id)
        {
            site_islem.AnketGonder(degerler, y_id);
        }
    }
}