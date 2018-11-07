using EduApi.Siniflar;
using EduApi.SiteModels;
using Newtonsoft.Json;
using SiteMvc.App_Classes;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SiteMvc.Controllers
{
    [AllActionRun]
    public class KurumsalKayitController : Controller
    {
        // GET: KurumsalKayit
        public ActionResult Index(string tip)
        {
            ViewBag.tip = tip;
            ViewBag.yazi = site_islem.SayfaGetir("Kurumsal-Kayit-Yazisi");
            return View();
        }
        [HttpPost]
        public PartialViewResult ProgramlariGetir(string egitim_id)
        {
            KurumsalKayitProgramGetirModel programlar = site_islem.KurumsalKayitProgramlariGetir(egitim_id);
            return PartialView(programlar);
        }
        [HttpPost]
        public string KurumsalKayitOkullar(string egitim_id)
        {
            List<ValueTextModel> gruplar = site_islem.EgitimTuruneGoreOkulGruplariGetir(egitim_id);
            return JsonConvert.SerializeObject(gruplar);
        }
        [HttpPost]
        public string Ekle(KurumsalKayitModel data)
        {
            try
            {
                if (data.yetkili_telefon == null)
                    data.yetkili_telefon = "";
                if (data.kampussubeadi == null)
                    data.kampussubeadi = "";
                if (data.fakulte_adi == null)
                    data.fakulte_adi = "";
                if (data.tip.Equals("yetkili"))
                    return site_islem.KurumsalKayitYetkiliEkle(data) ? "1" : "0";
                else if (HttpContext.Session["giris_yapan"] != null)
                    return site_islem.KurumsalKayitOgrenciEkle(data, HttpContext.Session["giris_yapan"].ToString()) ? "1" : "0";
                else
                    return "0";
            }
            catch (Exception e)
            {
                islem.MailGonder("webyazilim@dogruyer.com.tr", e.Message, "Kurumsal Kayıt Hataya Düştü");
                return "0";
            }
        }
        [HttpPost]
        public string Fakulteler()
        {
            List<ValueTextModel> fakulteler = site_islem.FakulteleriGetir(HttpContext.Session["Dil"].ToString());
            return JsonConvert.SerializeObject(fakulteler);
        }
    }
}